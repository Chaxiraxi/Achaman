using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using static Achaman.Console.Executor;
using Achaman.Console;
using Achaman;

namespace Achaman.Console
{
    public class ConsoleCommandInfo
    {
        public string Command { get; set; }
        public string Description { get; set; }
        public MethodInfo Method { get; set; }
        public ParameterInfo[] Parameters { get; set; }
    }

    public static class DebugConsoleCommandCollector
    {
        /// <summary>
        /// Collects all console commands from the current AppDomain.
        /// </summary>
        /// <returns>A list of ConsoleCommandInfo containing command details.</returns>
        public static List<ConsoleCommandInfo> CollectConsoleCommands()
        {
            var commands = new List<ConsoleCommandInfo>
            {
                new ConsoleCommandInfo
                {
                    Command = "help",
                    Description = "Displays a list of available console commands.",
                    Method = typeof(AdditionalCommands).GetMethod("HelpCommand", BindingFlags.Public | BindingFlags.Static),
                    Parameters = new ParameterInfo[] { }
                },
                // Manually add help, bind and unbind commands
                new ConsoleCommandInfo
                {
                    Command = "bind",
                    Description = "Binds a key to a console command.",
                    Method = typeof(AdditionalCommands).GetMethod("BindCommand", BindingFlags.Public | BindingFlags.Static),
                    Parameters = typeof(AdditionalCommands).GetMethod("BindCommand", BindingFlags.Public | BindingFlags.Static).GetParameters()
                },
                new ConsoleCommandInfo
                {
                    Command = "unbind",
                    Description = "Unbinds a key from a console command.",
                    Method = typeof(AdditionalCommands).GetMethod("UnbindCommand", BindingFlags.Public | BindingFlags.Static),
                    Parameters = typeof(AdditionalCommands).GetMethod("UnbindCommand", BindingFlags.Public | BindingFlags.Static).GetParameters()
                },
                new ConsoleCommandInfo
                {
                    Command = "bindings",
                    Description = "Lists all key bindings.",
                    Method = typeof(AdditionalCommands).GetMethod("GetAllBindings", BindingFlags.Public | BindingFlags.Static),
                    Parameters = new ParameterInfo[] { }
                },
                new ConsoleCommandInfo
                {
                    Command = "clear",
                    Description = "Clears the console output.",
                    Method = typeof(ConsoleGUI).GetMethod("ClearConsoleOutput", BindingFlags.Public | BindingFlags.Static),
                    Parameters = new ParameterInfo[] { }
                }
            };

            if (!AchamanPlugin.SHOULD_DISABLE_PROGRESS)
            {
                commands.Add(new ConsoleCommandInfo
                {
                    Command = "disable_progress",
                    Description = "Disables progression for the whole session using Void Manager.",
                    Method = typeof(VoidManager.Progression.ProgressionHandler).GetMethod("DisableProgression", BindingFlags.Public | BindingFlags.Static),
                    Parameters = typeof(VoidManager.Progression.ProgressionHandler).GetMethod("DisableProgression", BindingFlags.Public | BindingFlags.Static).GetParameters()
                });

                commands.Add(new ConsoleCommandInfo
                {
                    Command = "enable_progress",
                    Description = "Enables progression for the whole session using Void Manager.",
                    Method = typeof(VoidManager.Progression.ProgressionHandler).GetMethod("EnableProgression", BindingFlags.NonPublic | BindingFlags.Static),
                    // Parameters = typeof(VoidManager.Progression.ProgressionHandler).GetMethod("EnableProgression", BindingFlags.NonPublic | BindingFlags.Static).GetParameters()
                    Parameters = new ParameterInfo[] { }
                });
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                    {
                        var attribute = method.GetCustomAttributes(false)
                            .FirstOrDefault(attr => attr.GetType().Name == "ConsoleMethodAttribute");

                        if (attribute != null)
                        {
                            // Use reflection to access the properties
                            var attrType = attribute.GetType();
                            var commandProp = attrType.GetProperty("Command");

                            var descProp = attrType.GetProperty("Description");

                            string command = commandProp?.GetValue(attribute) as string ?? method.Name;
                            if (command.Equals("bind", StringComparison.OrdinalIgnoreCase) || command.Equals("unbind", StringComparison.OrdinalIgnoreCase))
                            {
                                // Skip 'bind' and 'unbind' commands
                                continue;
                            }
                            string description = descProp?.GetValue(attribute) as string ?? "";

                            commands.Add(new ConsoleCommandInfo
                            {
                                Command = command,
                                Description = description,
                                Method = method,
                                Parameters = method.GetParameters()
                            });
                            // Log the command for debugging purposes
                            AchamanPlugin.Logger.LogInfo($"Found command: {command} - {description}");
                        }
                    }
                }
            }

            return commands;
        }
    }
}

internal static class AdditionalCommands
{
    public static string HelpCommand()
    {
        if (allCommands == null)
        {
            allCommands = DebugConsoleCommandCollector.CollectConsoleCommands();
        }

        var helpText = new List<string>();
        foreach (var command in allCommands)
        {
            helpText.Add($"{command.Command} - {command.Description}");
        }
        return string.Join(Environment.NewLine, helpText);
    }

    /// <summary>
    /// Binds a command to a specific key
    /// </summary>
    /// <param name="key">The key to bind to</param>
    /// <param name="command">The command to execute when the key is pressed</param>
    /// <returns>Message indicating the binding result</returns>
    public static string BindCommand(string key, string command)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(command))
            return "Usage: bind <key> <command>";

        KeyCode keyCode;
        if (!TryParseKey(key, out keyCode))
            return $"Invalid key: {key}";

        if (!keyBindings.ContainsKey(keyCode))
            keyBindings[keyCode] = new List<string>();

        keyBindings[keyCode].Add(command);
        return $"Bound '{command}' to key '{key}'";
    }

    /// <summary>
    /// Removes all bindings for a specific key
    /// </summary>
    /// <param name="key">The key to unbind</param>
    /// <returns>Message indicating the unbinding result</returns>
    public static string UnbindCommand(string key)
    {
        if (string.IsNullOrEmpty(key))
            return "Usage: unbind <key>";

        KeyCode keyCode;
        if (!TryParseKey(key, out keyCode))
            return $"Invalid key: {key}";

        if (keyBindings.ContainsKey(keyCode))
        {
            int count = keyBindings[keyCode].Count;
            keyBindings.Remove(keyCode);
            return $"Unbound {count} command(s) from key '{key}'";
        }

        return $"No binding found for key '{key}'";
    }

    /// <summary>
    /// Gets all current key bindings
    /// </summary>
    /// <returns>A string containing all current key bindings</returns>
    public static string GetAllBindings()
    {
        if (keyBindings.Count == 0)
            return "No key bindings set";

        List<string> bindingStrings = new List<string>();
        foreach (var keyPair in keyBindings)
        {
            foreach (string command in keyPair.Value)
            {
                bindingStrings.Add($"{keyPair.Key}: {command}");
            }
        }

        return string.Join(Environment.NewLine, bindingStrings);
    }

    /// <summary>
    /// Disables progression for the whole session using Void Manager.
    /// </summary>
    public static void DisableProgression()
    {
        VoidManager.Progression.ProgressionHandler.DisableProgression(PluginInfo.PLUGIN_GUID);
    }
}
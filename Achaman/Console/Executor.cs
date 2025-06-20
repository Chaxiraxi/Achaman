using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using UnityEngine;

// TODO:
// Focus text field (again) when console is opened
// Find a way to prevent keys input from being processed by the game when the console is open
// Setup a centralized general key binding system that can be used anywhere in the plugin
// IMPORTANT TODO: Disable key bindings when the game chat is open
namespace Achaman.Console
{
    public static class Executor
    {
        public static List<ConsoleCommandInfo> allCommands;
        private static List<string> commandHistory = new List<string>();
        private static int historyIndex = -1;
        // Update to use KeyCode instead of ConsoleKey for better Unity integration
        public static Dictionary<KeyCode, List<string>> keyBindings = new Dictionary<KeyCode, List<string>>();
        /// <summary>
        /// Executes a console command
        /// </summary>
        public static string ExecuteCommand(string input, string[] args)
        {
            if (allCommands == null)
            {
                allCommands = DebugConsoleCommandCollector.CollectConsoleCommands();
            }

            // Merge quoted arguments into single arguments and remove quotes
            var processedArgs = new List<string>();
            bool inQuotes = false;
            string currentArg = "";
            foreach (var arg in args)
            {
                if (!inQuotes)
                {
                    if (arg.StartsWith("\"") && !arg.EndsWith("\""))
                    {
                        inQuotes = true;
                        currentArg = arg.TrimStart('\"');
                    }
                    else if (arg.StartsWith("\"") && arg.EndsWith("\""))
                    {
                        processedArgs.Add(arg.Trim('\"'));
                    }
                    else
                    {
                        processedArgs.Add(arg);
                    }
                }
                else
                {
                    currentArg += " " + arg;
                    if (arg.EndsWith("\""))
                    {
                        inQuotes = false;
                        processedArgs.Add(currentArg.TrimEnd('\"'));
                        currentArg = "";
                    }
                }
            }
            if (inQuotes && !string.IsNullOrEmpty(currentArg))
            {
                processedArgs.Add(currentArg); // Add any remaining arg if quotes were unbalanced
            }

            var cmd = allCommands.FirstOrDefault(c => c.Command.Equals(input, StringComparison.OrdinalIgnoreCase));
            if (cmd == null)
            {
                return $"Command not found: {input}";
            }

            object[] parsedArgs = ParseArguments(cmd.Parameters, processedArgs.ToArray());
            var result = cmd.Method.Invoke(null, parsedArgs);

            // record in history, but avoid duplicates
            var full = input + (processedArgs.Count > 0 ? " " + string.Join(" ", processedArgs) : "");

            // Only add to history if it's different from the last command
            if (commandHistory.Count == 0 || !commandHistory[commandHistory.Count - 1].Equals(full, StringComparison.OrdinalIgnoreCase))
            {
                commandHistory.Add(full);
            }
            historyIndex = commandHistory.Count;

            return result != null ? $"Command result: {result}" : "";
        }
        public static string PreviousHistory()
        {
            if (commandHistory.Count == 0) return "";
            historyIndex = Math.Max(0, historyIndex - 1);
            return commandHistory[historyIndex];
        }

        public static string NextHistory()
        {
            if (commandHistory.Count == 0) return "";
            historyIndex = Math.Min(commandHistory.Count, historyIndex + 1);
            return historyIndex < commandHistory.Count ? commandHistory[historyIndex] : "";
        }

        public static string GetAutoComplete(string input)
        {
            if (allCommands == null)
                allCommands = DebugConsoleCommandCollector.CollectConsoleCommands();

            var tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Special handling for 'bind' command
            if (tokens.Length >= 2 && tokens[0].Equals("bind", StringComparison.OrdinalIgnoreCase))
            {
                // If user is typing the command to bind (3rd token or more), recursively autocomplete the command part
                if (tokens.Length >= 3)
                {
                    // Reconstruct the command part after the key
                    string commandPart = string.Join(" ", tokens.Skip(2));
                    string autoCompleted = GetAutoComplete(commandPart);

                    // Only replace the command part if it actually autocompleted something
                    if (!string.Equals(commandPart, autoCompleted, StringComparison.OrdinalIgnoreCase))
                        return $"bind {tokens[1]} {autoCompleted}";
                    else
                        return input;
                }
                // If user is typing the key or just typed 'bind', do not autocomplete further
                return input;
            }

            // Default: autocomplete the command itself
            var matches = allCommands
                .Select(c => c.Command)
                .Where(cmd => cmd.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count == 0)
                return input;
            if (matches.Count == 1)
                return matches[0];

            // Find common prefix
            string prefix = matches[0];
            foreach (var match in matches)
            {
                int i = 0;
                while (i < prefix.Length && i < match.Length &&
                       char.ToLowerInvariant(prefix[i]) == char.ToLowerInvariant(match[i]))
                {
                    i++;
                }
                prefix = prefix.Substring(0, i);
                if (prefix.Length == 0)
                    break;
            }
            return prefix;
        }

        /// <summary>
        /// Parses the string arguments into the types required by the command parameters.
        /// </summary>
        private static object[] ParseArguments(System.Reflection.ParameterInfo[] parameters, string[] args)
        {
            var parsed = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i < args.Length)
                {
                    parsed[i] = Convert.ChangeType(args[i], parameters[i].ParameterType);
                }
                else
                {
                    parsed[i] = parameters[i].HasDefaultValue ? parameters[i].DefaultValue : null;
                }
            }
            return parsed;
        }

        /// <summary>
        /// Tries to parse a string representation of a key to a Unity KeyCode
        /// </summary>
        internal static bool TryParseKey(string keyString, out KeyCode keyCode)
        {
            keyCode = KeyCode.None;
            if (string.IsNullOrEmpty(keyString))
                return false;

            // Try parsing the key directly
            if (Enum.TryParse(keyString, true, out keyCode))
                return true;

            // Handle common aliases
            switch (keyString.ToLower())
            {
                case "enter":
                case "return":
                    keyCode = KeyCode.Return;
                    return true;
                case "esc":
                    keyCode = KeyCode.Escape;
                    return true;
                case "space":
                case "spacebar":
                    keyCode = KeyCode.Space;
                    return true;
                case "tab":
                    keyCode = KeyCode.Tab;
                    return true;
                default:
                    // For single character keys
                    if (keyString.Length == 1)
                    {
                        char c = char.ToUpper(keyString[0]);
                        if (char.IsDigit(c))
                        {
                            // Alpha numeric keys
                            keyCode = (KeyCode)((int)KeyCode.Alpha0 + (c - '0'));
                            return true;
                        }
                        else if (char.IsLetter(c))
                        {
                            keyCode = (KeyCode)((int)KeyCode.A + (c - 'A'));
                            return true;
                        }
                    }
                    return false;
            }
        }

        /// <summary>
        /// Checks if any keys with bindings are pressed and executes the bound commands
        /// </summary>
        /// <returns>The results of the command executions</returns>
        public static string CheckKeyBindings()
        {
            if (ConsoleGUI.IsVisible || SettingsGUI.isProgressDisabled) return string.Empty; // Prevent execution when progress is not disabled
            List<string> results = new List<string>();

            // Check each key in the keyBindings dictionary
            foreach (var keyPair in keyBindings)
            {
                if (UnityInput.Current.GetKeyDown(keyPair.Key))
                {
                    foreach (string command in keyPair.Value)
                    {
                        string[] parts = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length > 0)
                        {
                            string cmd = parts[0];
                            string[] args = parts.Length > 1 ? parts.Skip(1).ToArray() : new string[0];
                            string result = ExecuteCommand(cmd, args);
                            if (!string.IsNullOrEmpty(result)) results.Add(result);
                        }
                    }
                }
            }

            return string.Join(Environment.NewLine, results);
        }
    }
}
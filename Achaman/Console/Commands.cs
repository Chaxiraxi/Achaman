using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

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
            var commands = new List<ConsoleCommandInfo>();

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
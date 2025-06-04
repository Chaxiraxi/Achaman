using System;
using System.Collections.Generic;
using System.Linq;

namespace Achaman.Console
{
    public static class Executor
    {
        private static List<ConsoleCommandInfo> allCommands;
        private static List<string> commandHistory = new List<string>();
        private static int historyIndex = -1;
        /// <summary>
        /// Executes a console command
        /// </summary>
        public static void ExecuteCommand(string input, string[] args)
        {
            if (allCommands == null)
            {
                allCommands = DebugConsoleCommandCollector.CollectConsoleCommands();
                AchamanPlugin.Logger.LogInfo($"Collected {allCommands.Count} console commands.");
            }

            var cmd = allCommands.FirstOrDefault(c => c.Command.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var command in allCommands)
                {
                    AchamanPlugin.Logger.LogInfo($"{command.Command} - {command.Description}");
                }
                return;
            }

            if (cmd == null)
            {
                AchamanPlugin.Logger.LogWarning($"Command not found: {input}");
                return;
            }

            object[] parsedArgs = ParseArguments(cmd.Parameters, args);
            var result = cmd.Method.Invoke(null, parsedArgs);
            if (result != null)
            {
                AchamanPlugin.Logger.LogInfo($"Command result: {result}");
            }

            // record in history
            var full = input + (args.Length > 0 ? " " + string.Join(" ", args) : "");
            commandHistory.Add(full);
            historyIndex = commandHistory.Count;
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

            var match = allCommands
                .Select(c => c.Command)
                .FirstOrDefault(cmd => cmd.StartsWith(input, StringComparison.OrdinalIgnoreCase));
            return match ?? input;
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
    }
}
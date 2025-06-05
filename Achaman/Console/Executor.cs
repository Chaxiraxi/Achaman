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

            if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                var helpText = string.Join(
                    Environment.NewLine,
                    allCommands.Select(command => $"{command.Command} - {command.Description}")
                );
                return helpText;
            }

            var cmd = allCommands.FirstOrDefault(c => c.Command.Equals(input, StringComparison.OrdinalIgnoreCase));
            if (cmd == null)
            {
                return $"Command not found: {input}";
            }

            object[] parsedArgs = ParseArguments(cmd.Parameters, processedArgs.ToArray());
            var result = cmd.Method.Invoke(null, parsedArgs);

            // record in history
            var full = input + (processedArgs.Count > 0 ? " " + string.Join(" ", processedArgs) : "");
            commandHistory.Add(full);
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
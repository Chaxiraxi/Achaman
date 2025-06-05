using UnityEngine;
using System.Linq;

namespace Achaman.Console
{
    public class ConsoleGUI : MonoBehaviour
    {
        public static bool IsVisible { get; set; } = false;
        private string consoleInput = "";
        private Rect windowRect = new Rect(20, 20, 1500, 1000); // Adjusted height
        private const int WindowId = 12345; // Unique ID for the window
        private string consoleOutput = "";

        void OnGUI()
        {
            if (!IsVisible) return;

            windowRect = GUILayout.Window(WindowId, windowRect, DrawConsoleWindow, "Achaman Console");
        }

        void DrawConsoleWindow(int windowID)
        {
            GUILayout.BeginVertical();

            consoleOutput = GUILayout.TextArea(consoleOutput, GUILayout.ExpandHeight(true));

            // name the next control so we can focus it
            GUI.SetNextControlName("ConsoleInput");
            consoleInput = GUILayout.TextField(consoleInput, GUILayout.ExpandWidth(true));

            // immediately give it focus once the GUI has been laid out
            if (Event.current.type == EventType.Repaint)
                GUI.FocusControl("ConsoleInput");

            Event e = Event.current;
            if (GUI.GetNameOfFocusedControl() == "ConsoleInput" && e.type == EventType.KeyDown)
            {
                bool execute = false;
                if (e.keyCode == KeyCode.UpArrow)
                {
                    consoleInput = Executor.PreviousHistory();
                    e.Use();
                }
                else if (e.keyCode == KeyCode.DownArrow)
                {
                    consoleInput = Executor.NextHistory();
                    e.Use();
                }
                else if (e.keyCode == KeyCode.Tab)
                {
                    consoleInput = Executor.GetAutoComplete(consoleInput);
                    // Move cursor to end of text field after autocomplete
                    TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                    if (editor != null)
                    {
                        editor.cursorIndex = consoleInput.Length;
                        editor.selectIndex = consoleInput.Length;
                    }
                    e.Use();
                }
                else if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)
                {
                    execute = true;
                    e.Use();
                }

                if (execute)
                {
                    ExecuteCurrentCommand();
                }
            }

            if (GUILayout.Button("Execute"))
            {
                ExecuteCurrentCommand();
            }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        private void ExecuteCurrentCommand()
        {
            if (!string.IsNullOrWhiteSpace(consoleInput))
            {
                string[] parts = consoleInput.Trim().Split(' ');
                string command = parts[0];
                string[] args = parts.Skip(1).ToArray();
                var result = Executor.ExecuteCommand(command, args);
                AchamanPlugin.Logger.LogInfo($"Executing command: {command} with args: {string.Join(", ", args)}");
                AchamanPlugin.Logger.LogInfo($"Command result: {result}");
                consoleOutput += $"\n> {consoleInput}\n{result}";
                consoleInput = ""; // Clear input after execution
            }
        }
    }
}

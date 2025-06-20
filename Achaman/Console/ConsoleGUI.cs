using UnityEngine;
using System.Linq;

namespace Achaman.Console
{
    public class ConsoleGUI : MonoBehaviour
    {
        public static bool IsVisible { get; set; } = false;
        private string consoleInput = "";
        private Rect windowRect;
        private const int WindowId = 12345; // Unique ID for the window
        private string consoleOutput = "";

        // add this:
        private Vector2 scrollPosition = Vector2.zero;

        // ─── new fields for resizing ─────────────────────────────
        private bool isResizing = false;
        private Vector2 resizeStartMouse;
        private Vector2 resizeStartSize;
        private const float MinWidth = 200f;
        private const float MinHeight = 100f;
        private const float ResizeBorderSize = 10f;

        private bool initialized = false;
        private static ConsoleGUI Instance;
        private const float WindowProportion = 0.35f; // 35% of screen size

        void OnGUI()
        {
            if (!IsVisible || !SettingsGUI.isProgressDisabled) return;

            // Initialize windowRect to 35% of screen size, only once
            if (!initialized)
            {
                float width = Mathf.Max(MinWidth, Screen.width * WindowProportion);
                float height = Mathf.Max(MinHeight, Screen.height * WindowProportion);
                windowRect = new Rect(20, 20, width, height);
                Instance = this; // Set the static instance
                initialized = true;
            }

            windowRect = GUILayout.Window(WindowId, windowRect, DrawConsoleWindow, "Achaman Console");
        }

        void DrawConsoleWindow(int windowID)
        {
            GUILayout.BeginVertical();

            // replace the editable TextArea with this scrollable, read-only area:
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));
            GUI.enabled = false;
            GUILayout.TextArea(consoleOutput, GUILayout.ExpandHeight(true));
            GUI.enabled = true;
            GUILayout.EndScrollView();

            Event e = Event.current;
            // Handle history navigation BEFORE drawing the TextField
            if (GUI.GetNameOfFocusedControl() == "ConsoleInput" && e.type == EventType.KeyDown)
            {
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
            }

            // name the next control so we can focus it
            GUI.SetNextControlName("ConsoleInput");
            consoleInput = GUILayout.TextField(consoleInput, GUILayout.ExpandWidth(true));

            if (e.type == EventType.KeyDown && GUI.GetNameOfFocusedControl() == "ConsoleInput")
            {
                bool execute = false;
                if (e.keyCode == KeyCode.Tab)
                {
                    consoleInput = Executor.GetAutoComplete(consoleInput);
                    e.Use();

                    // immediately focus the input so TextEditor exists for that control
                    GUI.FocusControl("ConsoleInput");
                }
                // Broken because the game opens game chat instead
                // else if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)
                // {
                //     execute = true;
                //     e.Use();
                // }

                if (execute)
                {
                    ExecuteCurrentCommand();
                }
            }

            // on repaint, if our field is focused, push the cursor to the end
            if (Event.current.type == EventType.Repaint && GUI.GetNameOfFocusedControl() == "ConsoleInput")
            {
                var editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                if (editor != null)
                {
                    editor.cursorIndex = consoleInput.Length;
                    editor.selectIndex = consoleInput.Length;
                }
            }

            if (GUILayout.Button("Execute"))
            {
                ExecuteCurrentCommand();
            }

            GUILayout.EndVertical();

            // ─── resize handle ───────────────────────────────────────
            Rect resizeRect = new Rect(
                windowRect.width - ResizeBorderSize,
                windowRect.height - ResizeBorderSize,
                ResizeBorderSize,
                ResizeBorderSize
            );
            GUI.Box(resizeRect, "");

            if (e.type == EventType.MouseDown && resizeRect.Contains(e.mousePosition))
            {
                isResizing = true;
                resizeStartMouse = e.mousePosition;
                resizeStartSize = new Vector2(windowRect.width, windowRect.height);
                e.Use();
            }
            if (isResizing)
            {
                if (e.type == EventType.MouseDrag)
                {
                    float w = Mathf.Max(MinWidth, resizeStartSize.x + (e.mousePosition.x - resizeStartMouse.x));
                    float h = Mathf.Max(MinHeight, resizeStartSize.y + (e.mousePosition.y - resizeStartMouse.y));
                    windowRect.width = w;
                    windowRect.height = h;
                    e.Use();
                }
                else if (e.type == EventType.MouseUp)
                {
                    isResizing = false;
                    e.Use();
                }
            }

            GUI.DragWindow();
        }

        public static void ClearConsoleOutput()
        {
            ConsoleGUI.Instance.consoleOutput = "";
            ConsoleGUI.Instance.scrollPosition = Vector2.zero; // Reset scroll position
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

                // Scroll to the bottom after adding new content
                // We need to defer this to next frame because the UI needs to update first
                StartCoroutine(ScrollToBottomNextFrame());
            }
        }

        private System.Collections.IEnumerator ScrollToBottomNextFrame()
        {
            // Wait for the end of the frame so the UI can update
            yield return new WaitForEndOfFrame();

            // Set scroll position to bottom
            scrollPosition = new Vector2(0, float.MaxValue);
        }
    }
}

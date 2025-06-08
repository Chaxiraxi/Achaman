// Patch to disable ingame chat when the console is open
using Gameplay.Chat;
using HarmonyLib;

namespace Achaman.Console
{
    [HarmonyPatch(typeof(TextChat), "OpenChat")]
    internal class TextChatPatch
    {
        private static bool Prefix()
        {
            // If the console is visible, prevent the chat from opening
            return !ConsoleGUI.IsVisible;
        }
    }
}
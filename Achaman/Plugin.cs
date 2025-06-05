using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using VoidManager;
using VoidManager.MPModChecks;
using UnityEngine;
using Achaman.Console;

namespace Achaman
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.USERS_PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency(MyPluginInfo.PLUGIN_GUID)]
    public class AchamanPlugin : BaseUnityPlugin
    {
        public const bool SHOULD_DISABLE_PROGRESS = false;

        internal static new ManualLogSource Logger;
        public static AchamanPlugin Instance { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "N/A")]
        private void Awake()
        {
            Instance = this;
            // Initialize settings
            Logger = base.Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginInfo.PLUGIN_GUID);
            base.Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void Update()
        {
            if (UnityInput.Current.GetKeyDown(Settings.ConsoleToggleKey))
            {
                ConsoleGUI.IsVisible = !ConsoleGUI.IsVisible;
            }
        }
    }

    public class VoidManagerPlugin : VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Host;
        public override string Author => PluginInfo.PLUGIN_AUTHORS;
        public override string Description => PluginInfo.PLUGIN_DESCRIPTION;
        // public override string ThunderstoreID => PluginInfo.PLUGIN_THUNDERSTORE_ID;
        public static bool isHosting { get; private set; } = false;
        private static GameObject consoleGameObject;

        public override SessionChangedReturn OnSessionChange(SessionChangedInput input)
        {
            isHosting = input.IsHost;
            if (!isHosting && AchamanPlugin.SHOULD_DISABLE_PROGRESS) Settings.Reset();

            // Create a new GameObject for the ConsoleGUI and add the script to it
            if (consoleGameObject == null)
            {
                consoleGameObject = new GameObject("AchamanConsoleGUI");
                consoleGameObject.AddComponent<ConsoleGUI>();
                Object.DontDestroyOnLoad(consoleGameObject); // Keep the console GUI across scene loads
            }

            return base.OnSessionChange(input);
        }
    }
}
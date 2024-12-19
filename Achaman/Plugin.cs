using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using VoidManager;
using VoidManager.MPModChecks;

namespace Achaman {
    
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.USERS_PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency(VoidManager.MyPluginInfo.PLUGIN_GUID)]
    public class BepinPlugin : BaseUnityPlugin {

        internal static new ManualLogSource Logger;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "N/A")]
        private void Awake() {
            Logger = base.Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginInfo.PLUGIN_GUID);
            base.Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }


    public class VoidManagerPlugin : VoidPlugin {
        public override MultiplayerType MPType => MultiplayerType.Session;
        public override string Author => PluginInfo.PLUGIN_AUTHORS;
        public override string Description => PluginInfo.PLUGIN_DESCRIPTION;
        // public override string ThunderstoreID => PluginInfo.PLUGIN_THUNDERSTORE_ID;

        // public override SessionChangedReturn OnSessionChange(SessionChangedInput input) {
        //     switch (input.CallType) {
        //         case CallType.SessionEscalated:
        //         case CallType.HostStartSession:
        //             VoidManager.Progression.ProgressionHandler.DisableProgression(MyPluginInfo.PLUGIN_GUID);
        //             break;
        //         default:
        //             break;
        //     }
        //     return base.OnSessionChange(input);
        // }
    }
}
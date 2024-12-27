using HarmonyLib;

namespace Achaman.Patches {
    [HarmonyPatch(typeof(FlyJetpack))]
    internal class FlyJetpackPatch {
        [HarmonyPostfix]
        [HarmonyPatch("get_ThrustForceMultiplier")]
        private static void ThrustForceMultiplier(ref float __result) {
            __result *= Settings.JetpackThrustMultiplier;
        }
 
        [HarmonyPostfix]
        [HarmonyPatch("get_DashForceMultiplier")]
        private static void DashForceMultiplier(ref float __result) {
            __result *= Settings.JetpackDashForceMultiplier;
        }
    }

    [HarmonyPatch(typeof(CharacterOxygen), "get_IgnoreOxygen")]
    internal class CharacterOxygenPatch {
        [HarmonyPostfix]
        private static void IgnoreOxygen(ref bool __result) {
            __result = true;
        }
    }
}

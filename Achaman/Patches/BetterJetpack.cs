using HarmonyLib;

namespace Achaman.Patches {
    [HarmonyPatch(typeof(FlyJetpack), "get_ThrustForceMultiplier")]
    internal class MoreThrust {
        [HarmonyPostfix]
        private static void ThrustForceMultiplier(ref float __result) {
            __result *= Settings.JetpackThrustMultiplier;
        }
    }

    [HarmonyPatch(typeof(FlyJetpack), "get_DashForceMultiplier")]
    internal class MoreDash {
        [HarmonyPostfix]
        private static void DashForceMultiplier(ref float __result) {
            __result *= Settings.JetpackDashForceMultiplier;
        }
    }
}
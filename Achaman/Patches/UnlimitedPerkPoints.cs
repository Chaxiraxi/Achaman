using HarmonyLib;

namespace Achaman.Patches {
    [HarmonyPatch(typeof(CG.Profile.PlayerProfile), "get_TotalOwnedPerkPoints")]
    internal class UnlimitedPerkPointsPatch {
        [HarmonyPostfix]
        private static void UnlimitedPerkPoints(ref int __result, CG.Profile.PlayerProfile __instance) {
            Settings.Defaults.TotalOwnedPoints = __result;
            __result = (int) Settings.TotalOwnedPoints;
        }
    }
}
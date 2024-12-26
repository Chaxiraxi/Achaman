using HarmonyLib;

namespace Achaman.Patches {
    [HarmonyPatch(typeof(Gameplay.Power.ProtectedPowerSystem), "BreakNextBreaker")]
    internal class BreakerTripPatch {
        [HarmonyPrefix]
        private static bool BreakerTripDown() {
            return !Settings.NoBreakerTrip;
        }
    }
}
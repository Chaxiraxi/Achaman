using HarmonyLib;

namespace Achaman.Patches {
    [HarmonyPatch(typeof(CG.Game.Player.Player), "ApplyHitDamage")]
    internal class PlayerInvincibilityPatch {
        [HarmonyPrefix]
        private static bool ApplyHitDamage() {
            return !Settings.NoPlayerDamage;
        }
    }
}
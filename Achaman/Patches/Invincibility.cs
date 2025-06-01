using CG.Space;
using HarmonyLib;

namespace Achaman.Patches
{
    [HarmonyPatch(typeof(CG.Game.Player.Player), "ApplyHitDamage")]
    internal class PlayerInvincibilityPatch
    {
        [HarmonyPrefix]
        private static bool ApplyHitDamage()
        {
            return !Settings.NoPlayerDamage;
        }
    }

    // DEPRECATED: This patch is no longer needed as the game has been updated to handle ship invincibility differently.
    // [HarmonyPatch(typeof(OrbitObject), "ApplyHitDamage")]
    // internal class ShipInvincibilityPatch {
    //     [HarmonyPrefix]
    //     private static bool ApplyHitDamage(OrbitObject __instance) {
    //         return !(Settings.NoShipDamage && __instance is AbstractPlayerControlledShip);
    //     }
    // }
}
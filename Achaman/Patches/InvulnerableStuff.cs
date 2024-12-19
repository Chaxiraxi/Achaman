using HarmonyLib;

namespace Achaman {
    // Make player invulnerable
    [HarmonyPatch(typeof(CG.Game.Player.Player), "set_HitPoints")]
    internal class PlayerHitPointsPatch {
        [HarmonyPrefix]
        private static bool SetHitPoints(ref float value, CG.Game.Player.Player __instance) {
            if (!Settings.NoPlayerDamage) return true;
            if (Settings.SetHealthToMax) {
                value = __instance.MaxHealth.Value;
            }
            return false;
        }
    }

    // Make Player's ship invulnerable
    [HarmonyPatch(typeof(CG.Space.OrbitObject), "set_HitPoints")]
    internal class ShipHitPointsPatch {
        [HarmonyPrefix]
        private static bool SetHitPoints(ref float value, CG.Space.OrbitObject __instance) {
            if (!Settings.NoShipDamage) return true;
            // Check if instance is instance of AbstractPlayerShip
            if (__instance is CG.Space.AbstractPlayerControlledShip playerShip) {
                if (Settings.SetHealthToMax) {
                    value = __instance.MaxHitPoints.Value;
                }
                return false;
            }
            return true;
        }
    }
}
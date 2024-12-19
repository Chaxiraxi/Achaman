using HarmonyLib;

namespace Achaman {
    [HarmonyPatch(typeof(ClassAbility), "ActivationKeyPressed")]
    internal class InfiniteAbility {
        [HarmonyPrefix]
        private static bool ActivationKeyPressed(ClassAbility __instance) {
            if (__instance is FiniteDurationClassAbility ability) {
                if (!Settings.InfiniteAbility) return true;

                if (ability.IsOngoing()) {
                    ability.StopAbility();
                    ability.CurrentCooldown = 0f;
                    return false;
                }
                
                ability.CurrentCooldown = 0f;
                ability.StartAbility();
                return false;
            }
            return true;
        }
    }
}
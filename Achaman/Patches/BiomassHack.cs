using HarmonyLib;

namespace Achaman.Patches {
    [HarmonyPatch(typeof(GameSessionSuppliesManager), "ModifyBiomassCount")]
    internal class BiomassHack {
        [HarmonyPrefix]
        private static void ModifyBiomassCount(ref int biomassAddition) {
            if (Settings.NoBiomassDepletion && biomassAddition < 0) {
                biomassAddition = 0;
            }

            if (Settings.BiomassMultiplier != 1f) {
                biomassAddition = (int)(biomassAddition * Settings.BiomassMultiplier);
            }
        } 
    }
}
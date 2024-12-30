using HarmonyLib;

namespace Achaman.Patches {
    // Mark items in the selection list as affordable
    [HarmonyPatch(typeof(FabricationTab), "CanAfford")]
    internal class CanAffordPatch {
        [HarmonyPostfix]
        private static void AffordEverything(ref bool __result) {
            if (!Settings.PrintForFree) return;
            __result = true;
        }
    }

    // Upgrade fabricator for free (mark its cost to 0)
    [HarmonyPatch(typeof(CG.Ship.Modules.FabricatorModule), "get_CurrentUpgradeCost")]
    internal class FabricatorModulePatch {
        [HarmonyPostfix]
        private static void UpgradeForFree(ref int __result) {
            if (!Settings.UpgradeFabricatorForFree) return;
            if (__result != -1) __result = 0;
        }
    }

    // Do not show error message when player can't afford the upgrade or the print
    [HarmonyPatch(typeof(FabricatorActionTab), "DisplayError")]
    internal class CheckErrorPatch {
        [HarmonyPrefix]
        private static bool DisplayError(string message) {
            if (!Settings.UpgradeFabricatorForFree) return true;
            return message != "#Fabricator_Error_CantAfford";
        }
    }

    [HarmonyPatch(typeof(Gameplay.Factory.CarryableFactoryLogic))]
    internal class CarryableFactoryLogicPatch {
        // Effectively print for free
        [HarmonyPrefix]
        [HarmonyPatch("Print")]
        private static void Print(ref int cost) {
            if (!Settings.PrintForFree) return;
            cost = 0;
        }

        // Multiply by 10 the alloy recycled
        [HarmonyPrefix]
        [HarmonyPatch("Recycle")]
        private static void Recycle(ref int amount) {
            if (Settings.MultiplyRecycledAlloy == 1f) return;
            amount = (int) (amount * Settings.MultiplyRecycledAlloy);
        }
    }

    [HarmonyPatch(typeof(FabricatorData), "CanPrint")]
    internal class FabricatorDataPatch {
        [HarmonyPostfix]
        private static void CanPrint(UI.LoadoutTerminal.PurchasableItem item, ref bool __result) {
            if (!Settings.PrintForFree) return;
            __result = item.CraftingRules.CraftingMethod == ResourceAssets.CraftMethod.Resource;
        }
    }
}
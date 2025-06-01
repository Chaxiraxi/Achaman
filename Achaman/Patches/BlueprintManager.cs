using HarmonyLib;
using CG.Ship.Modules;
using System.Collections.Generic;
using ResourceAssets;

namespace Achaman.Patches {

    [HarmonyPatch(typeof(FabricatorModule))]
    internal class FabricatorModuleBlueprintPatch {
        public static FabricatorModule fabricatorInstance { get; private set; }
        internal static List<GUIDUnion> blueprintsToLearn { private get; set; }
        internal static bool isLearningBlueprints { private get; set; }

        [HarmonyReversePatch]
        [HarmonyPatch("TryAddItemToSharedUnlockPool")]
        public static void LearnBlueprint(object instance, GUIDUnion carryableGuid, bool isPerkAddition) => throw new System.NotImplementedException("This is a stub method.");        

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        private static void RegisterFabricatorInstance(FabricatorModule __instance) => fabricatorInstance = __instance;

        // Uses the Update method to prevent the game from freezing while learning blueprints (there's 115 of them)
        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        private static void LearnBlueprintsOneByOne(FabricatorModule __instance) {
            if (isLearningBlueprints && blueprintsToLearn.Count > 0) {
                GUIDUnion guid = blueprintsToLearn[0];
                LearnBlueprint(__instance, guid, false);
                blueprintsToLearn.RemoveAt(0);
            } else if (isLearningBlueprints && blueprintsToLearn.Count == 0) {
                isLearningBlueprints = false;
                BepinPlugin.Logger.LogInfo("All blueprints learned.");
            }
        }
    }

    public class BlueprintManager
    {
        public static void LearnAllSavedBlueprints()
        {
            if (FabricatorModuleBlueprintPatch.fabricatorInstance == null)
            {
                BepinPlugin.Logger.LogError("Cannot learn blueprints: Fabricator instance is null");
                return;
            }

            var guidList = DumpGUIDs();
            if (guidList.Count == 0)
            {
                BepinPlugin.Logger.LogError("No blueprints found to learn.");
                return;
            }
            FabricatorModuleBlueprintPatch.blueprintsToLearn = guidList;
            FabricatorModuleBlueprintPatch.isLearningBlueprints = true;
        }

        public static List<GUIDUnion> DumpGUIDs() {
            var guidList = new List<GUIDUnion>();
            foreach (CraftableItemDef craftableItemDef in ResourceAssetContainer<CraftingDataContainer, global::UnityEngine.Object, CraftableItemDef>.Instance.AssetDescriptions)
            {
                GUIDUnion guid = craftableItemDef.AssetGuid;
                guidList.Add(guid);
            }
            return guidList;
        }

    }
}
using Gameplay.Mutators;
using HarmonyLib;
using ResourceAssets;

namespace Achaman.Patches
{
    [HarmonyPatch(typeof(Gameplay.Hub.HubMutatorManager), "GetMaxMutatorsCount")]
    internal class UnlimitedMutatorsPatch
    {
        [HarmonyPostfix]
        private static void UnlimitedMutators(ref int __result)
        {
            if (Settings.RemoveMutatorLimit)
            {
                int count = -1; // Start with -1 because it seems that there is a debug mutator that is not shown in the UI.
                foreach (MutatorDef mutatorDef in ResourceAssetContainer<MutatorContainer, Mutator, MutatorDef>.Instance.AssetDescriptions)
                {
                    count++;
                }
                __result = count;
            }
        }
    }
}
using HarmonyLib;
using CG.Ship.Modules;
using System.Collections.Generic;
using System.Linq;

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

    public class BlueprintManager {
        public static void LearnAllSavedBlueprints() {
            if (FabricatorModuleBlueprintPatch.fabricatorInstance == null) {
                BepinPlugin.Logger.LogError("Cannot learn blueprints: Fabricator instance is null");
                return;
            }

            var guidList = new List<GUIDUnion>();
            guidList.AddRange(itemGUIDs.Select(guid => new GUIDUnion(guid)));
            FabricatorModuleBlueprintPatch.blueprintsToLearn = guidList;
            FabricatorModuleBlueprintPatch.isLearningBlueprints = true;
        }

        private static string[] itemGUIDs = new string[] {
            "bc2dcf53afe3a90478b5d9fcffb1f523",
            "2f592cf15994875469eda092a4b159f0",
            "86d4f5371b6f29446a5795ac8eb41c70",
            "6b2c6be56b6678643bd0039305890622",
            "57a0ed79bfb068647a8fbef4bd619e1e",
            "5527af35a54518042b4c3712846f4bb2",
            "1f75966363b092449b9e066e9c4203d6",
            "9e7063ad7731f9f4ea3656e5b11eab6f",
            "0eed5aac9f671f246a26860a992b3b3b",
            "8c8b749b85d84524b84522731d829bb4",
            "3397103be0cadc741b9075796811f011",
            "b97c481e4b0364c4fbbabf493ce22edd",
            "a628be2251840f44b82c63abfa6dc63b",
            "4e2c7e5990ca28946bc455b633719d41",
            "77a5d0e9946f4604d9525010f5e3efa9",
            "ef0ac25ce2c355046a03588fba53bdc2",
            "01cad5c2da91b0a43a0dbeed5e1f7609",
            "0ace8d34465caca449aa6de115a3a887",
            "a850d26e3d89c2343af492c0b4156bfe",
            "77316e896fa40fe4da2bac1ec63c59a6",
            "b640f82a5eccc804b8c53b2e4d2cc0c7",
            "2ff7c02affc74104d830e6414a7358c1",
            "7a8d558be3b436b4cb0d509087db5b76",
            "0efd796e1f8698944a15b9f3549315ae",
            "22f4e71d21f61e347adbe3ae8aad0984",
            "05b87bc024515234bb42fdfd720e3ead",
            "5ab7ebf5da53a5d41b704a5eca6a3914",
            "be8589a7eb876bd4192f87d6ad2d956c",
            "a2d63eec500238e4a913111561f64dfe",
            "7c2a372f35a77f94dad45f589fc1ca07",
            "3596d1683e52ad24185c439dfea6d5ef",
            "62c6dae9ed6654245831e711b8c760a8",
            "35c8be9931e98e74095536f73f16e4b9",
            "9308228dae683ca409567695a1f7b3df",
            "637926b33a572df44a7f323dfecf984e",
            "ad8a2267b79337f4d9876417e39b2d3c",
            "e8fcb4b20bf9b8647b664227e20177e9",
            "b7acaa743df2515458e646ffce30f2bd",
            "6ed76c9aa0dbf994cb33cf7dcd679690",
            "bf2198cbcbca0fc4b92ab3cde3ba197d",
            "f12123ccb350d83449cb9b30fd00cc50",
            "20a6459d14744b245a07620cb739f2f6",
            "6a92cf16baab1304a95f85b6d27bab27",
            "00ed8fca5e8a53f47b0903425d4568ae",
            "6e877538ce61f0a42a31e75d800171e4",
            "8ea4f588a9983204fbe9762aa7878099",
            "330d1ccd5dd686444baa8ef350d069a2",
            "26a7321d6e9c4054b882a3259106ba60",
            "3fe52c39f11981a4d8ac9cbc45745034",
            "04f2c889309ac564988987e9ed58029d",
            "1f708c18fab8eb640ba6cd439166bc09",
            "e81a8a02f2f99894cbd1de3e26d5bd8a",
            "d99dd17ec81b6f3469469a800cde27fb",
            "70a351589a91f1c4d8a1f6094acb7041",
            "958bc563e6613984eb2b3dd3d307a526",
            "db0a40ea1d616fa4eb80b044dc3efa3e",
            "d2427729f742ec04d98aa89b0575e9e0",
            "ee69440bbce371e458daeba6eee12a49",
            "4df2946cea6f59f45b269facd0f681b9",
            "49991ea1005c0134cb8e8febcd0dfe8b",
            "072d0f489605db34ab5f970f055ce9ee",
            "1743d99b1ac3048479a3d6ef784cc89a",
            "97b1211d7675ea5488be8e2405977a3c",
            "ecaa9dd5965873a4e80e6e5f54292819",
            "36bdd74a21afc114e85b4bf195798c92",
            "8aa410e3179560f48b6bb0a88665cc6a",
            "7833d8c016b626941986d784dddc4de8",
            "240c3ff2b67a84d49a318455b062f920",
            "1f4ffd59a3537f44ba780ce747c7be82",
            "a1ec2fd7783f9894093b3714c50278d0",
            "c0757aca3b8daac4e8fd14fa428de570",
            "54628a6282108784689fa8def0e934b4",
            "ecc864682f34a0743b824ce24438fe01",
            "67819aed3fd8de240a7e3f610f1d3267",
            "0701afdfc3185e94fb05e68dc32e9a04",
            "9318e5d96f2f4784db8e3cb9b5f7816e",
            "56672971813f60b40ad3b7c8768b8a67",
            "4506041f9b9f52a4aba65e5881b2ae48",
            "80d5c80ef0b32ee4ba1398925b20e4bf",
            "0b35547f769f1d644a255e9459c5d520",
            "d7c5b330dde838f4da6eec0072cb3007",
            "1e129dea6222e7f44a3b87a55f75b292",
            "1de2d339fff94df4998e4f9b083813a2",
            "187e27fe63a37c847a0b1ae40e7898c5",
            "c85737fb51b82aa499eb24304bcc0ffd",
            "ff9b740e45637234d8096671010f5455",
            "8d348ab1ea1af454e88f9b98a6d6dab2",
            "5f342c44aaf4bad47a9e4809fbfea7be",
            "44c14583902327a42ba5a5ff7a5cdc25",
            "2ab68438c1f7e3e4c938f8eb81bebc45",
            "f12cdbbd7cf9fc24da46cdaf875382fc",
            "5d59d95a45ded8a48a32239c8f76711e",
            "294e4636542a3d74d928c1dd363e978a",
            "4ac9656bd45eec44db0d53876aac4bef",
            "6f57adbe28980c646bfae33264931667",
            "b4d3a9eb03c60944e94c0259df555cc4",
            "9e3dcbb56b454d543a8a0ccb4c1046a5",
            "7574c41d94088c44284275e915d50b5e",
            "ef88c0f1835ae58449e178a0a9eef028",
            "fb33abcf75780df42a053168c1ec08d3",
            "6810d852c8fc354458dd401c57fde7a1",
            "45497a905cb5edf4296930ac7f404a5f",
            "ecdce48a4cd45ad4682226093daa9e23",
            "9e463c28d0089874cab740efeb22a27e",
            "baf6cd1e2edfcd247a7ff26dd668794b",
            "6bbb315a85a7370488801ae7dbc3be6c",
            "4a7b0296ec4206942ab8a9f0936f37f2",
            "8eae49efd73eb844a93d4211a867c100",
            "843ef4b728effe343ae74df5dc6a4349",
            "3fa8f129f98add24b9fcb9400e3adaa4",
            "21d9405ddf7e03e459f61f4be43d0e62",
            "ceb44a360fc0c9e4bbdc4e1c9dcb0e37",
            "22d0f73214cc0b044bb5291dcfdb7b7b",
            "77ae3410d585e1245bb6ee866aff0a0f",
            "7e8742742dfccf74c854bda99cc55a17"
        };
    }
}
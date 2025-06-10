using HarmonyLib;
using Photon.Pun;

namespace Achaman.Patches
{
    [HarmonyPatch(typeof(ModdingUtils), nameof(ModdingUtils.PublishModRoomProperty))]
    // This implementation is kinda overkill ngl. Is the prefix really relevent when we're resetting the modding type in the postfix? Maybe it saves some processing time idk.
    public class SpoofVanillaRoom
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            // Prevent the Photon room from being marked as a modded room if the setting is enabled.
            if (Settings.SpoofVanillaRoom) ModdingUtils.SessionModdingType = ModdingType.none;
        }

        [HarmonyPostfix]
        public static void Postfix()
        {
            if (Settings.SpoofVanillaRoom)
            {
                // Remove the custom property from the Photon room to ensure it behaves like a vanilla room if it has been already set previously by the game.
                if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("R_Mod", out _))
                {
                    // Remove the custom property to spoof the room as vanilla.
                    PhotonNetwork.CurrentRoom.CustomProperties.Remove("R_Mod");
                    // Also remove the modding type to ensure it behaves like a vanilla room.
                    ModdingUtils.SessionModdingType = ModdingType.none;
                }
            }
        }
    }
}
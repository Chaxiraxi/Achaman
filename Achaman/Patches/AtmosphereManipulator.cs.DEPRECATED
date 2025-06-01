using CG.Space;
using CG.Ship;
using Gameplay.Atmosphere;
using HarmonyLib;
using Photon.Pun;

namespace Achaman.Patches {
    [HarmonyPatch(typeof(AbstractPlayerControlledShip), "ApplyPassiveTemperatureShift")]
    public class ApplyPassiveTemperatureShiftPatch {
        [HarmonyPrefix]
        public static bool ApplyPassiveTemperatureShift(AbstractPlayerControlledShip __instance) {
            if (!Settings.ManipulateAtmosphere) return true;

            MonoBehaviourPun photonView = __instance;
            if (photonView == null || !photonView.photonView.AmOwner) return true;
            if (!__instance.InteriorAtmosphere) return true;

            DictionarySerializable<Room, AtmosphereValues> roomAtmospheres = __instance.InteriorAtmosphere.RoomAtmospheres;
            for (int i = 0; i < roomAtmospheres.Count; i++) {
                AtmosphereValues elementAt = roomAtmospheres.GetElementAt(i);
                elementAt.Temperature = Settings.ShipTemperature;
                elementAt.Oxygen = Settings.ShipOxygen;
                elementAt.Pressure = Settings.ShipPressure;
                roomAtmospheres.SetElementAt(i, elementAt);
            }

            return false;
        }
    }
}
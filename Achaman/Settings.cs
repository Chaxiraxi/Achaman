using System;
using UnityEngine;

namespace Achaman
{
    internal static class Settings
    {
        public static bool InfiniteAbility { get; set; } = Defaults.InfiniteAbility;
        public static bool PrintForFree { get; set; } = Defaults.PrintForFree;
        public static bool UpgradeFabricatorForFree { get; set; } = Defaults.UpgradeFabricatorForFree;
        public static float MultiplyRecycledAlloy { get; set; } = Defaults.MultiplyRecycledAlloy;
        public static float JetpackThrustMultiplier { get; set; } = Defaults.JetpackThrustMultiplier;
        public static float JetpackDashForceMultiplier { get; set; } = Defaults.JetpackDashForceMultiplier;
        public static int TotalOwnedPoints { get; set; } = Defaults.TotalOwnedPoints;
        public static bool NoPlayerDamage { get; set; } = Defaults.NoPlayerDamage;
        public static bool ManipulateAtmosphere { get; set; } = Defaults.ManipulateAtmosphere;
        public static float ShipTemperature { get; set; } = Defaults.ShipTemperature;
        public static float ShipPressure { get; set; } = Defaults.ShipPressure;
        public static float ShipOxygen { get; set; } = Defaults.ShipOxygen;
        public static bool NoShipDamage { get; set; } = Defaults.NoShipDamage;
        public static bool NoBreakerTrip { get; set; } = Defaults.NoBreakerTrip;
        public static bool NoJetpackOxygenDepletion { get; set; } = Defaults.NoJetpackOxygenDepletion;
        public static bool NoBiomassDepletion { get; set; } = Defaults.NoBiomassDepletion;
        public static float BiomassMultiplier { get; set; } = Defaults.BiomassMultiplier;
        public static float FabricatorSpeedMultiplier = Defaults.FabricatorSpeedMultiplier;
        public static KeyCode ConsoleToggleKey { get; set; } = Defaults.ConsoleToggleKey;
        public static bool RemoveMutatorLimit { get; set; } = Defaults.RemoveMutatorLimit;

        internal static void Reset()
        {
            InfiniteAbility = Defaults.InfiniteAbility;
            PrintForFree = Defaults.PrintForFree;
            UpgradeFabricatorForFree = Defaults.UpgradeFabricatorForFree;
            MultiplyRecycledAlloy = Defaults.MultiplyRecycledAlloy;
            JetpackThrustMultiplier = Defaults.JetpackThrustMultiplier;
            JetpackDashForceMultiplier = Defaults.JetpackDashForceMultiplier;
            TotalOwnedPoints = Defaults.TotalOwnedPoints;
            NoPlayerDamage = Defaults.NoPlayerDamage;
            ManipulateAtmosphere = Defaults.ManipulateAtmosphere;
            ShipTemperature = Defaults.ShipTemperature;
            ShipPressure = Defaults.ShipPressure;
            ShipOxygen = Defaults.ShipOxygen;
            NoShipDamage = Defaults.NoShipDamage;
            NoBreakerTrip = Defaults.NoBreakerTrip;
            NoJetpackOxygenDepletion = Defaults.NoJetpackOxygenDepletion;
            NoBiomassDepletion = Defaults.NoBiomassDepletion;
            BiomassMultiplier = Defaults.BiomassMultiplier;
            FabricatorSpeedMultiplier = Defaults.FabricatorSpeedMultiplier;
            ConsoleToggleKey = Defaults.ConsoleToggleKey;
            RemoveMutatorLimit = Defaults.RemoveMutatorLimit;
        }

        internal static class Defaults
        {
            public const bool InfiniteAbility = false;
            public const bool PrintForFree = false;
            public const bool UpgradeFabricatorForFree = false;
            public const float MultiplyRecycledAlloy = 1f;
            public const float JetpackThrustMultiplier = 1f;
            public const float JetpackDashForceMultiplier = 1f;
            public static int TotalOwnedPoints = 31;
            public const bool NoPlayerDamage = false;
            public const bool ManipulateAtmosphere = false;
            public const float ShipTemperature = 21f;
            public const float ShipPressure = 1f;
            public const float ShipOxygen = 1f;
            public const bool NoShipDamage = false;
            public const bool NoBreakerTrip = false;
            public const bool NoJetpackOxygenDepletion = false;
            public const bool NoBiomassDepletion = false;
            public const float BiomassMultiplier = 1f;
            public const float FabricatorSpeedMultiplier = 1f;
            public const KeyCode ConsoleToggleKey = KeyCode.F2;
            public const bool RemoveMutatorLimit = false;
        }
    }
}

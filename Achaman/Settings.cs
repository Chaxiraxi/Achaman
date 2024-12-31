using System;

namespace Achaman {
    internal static class Settings {
        public static bool InfiniteAbility { get; set; } = Defaults.InfiniteAbility;
        public static bool PrintForFree { get; set; } = Defaults.PrintForFree;
        public static bool UpgradeFabricatorForFree { get; set; } = Defaults.UpgradeFabricatorForFree;
        public static float MultiplyRecycledAlloy { get; set; } = Defaults.MultiplyRecycledAlloy;
        public static float JetpackThrustMultiplier { get; set; } = Defaults.JetpackThrustMultiplier;
        public static float JetpackDashForceMultiplier { get; set; } = Defaults.JetpackDashForceMultiplier;
        public static float TotalOwnedPoints { get; set; } = Defaults.TotalOwnedPoints;
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

        internal static void Reset() {
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
        }

        internal static class Defaults {
            public static bool InfiniteAbility { get; } = false;
            public static bool PrintForFree { get; } = false;
            public static bool UpgradeFabricatorForFree { get; } = false;
            public static float MultiplyRecycledAlloy { get; } = 1f;
            public static float JetpackThrustMultiplier { get; } = 1f;
            public static float JetpackDashForceMultiplier { get; } = 1f;
            public static float TotalOwnedPoints { get; set; } = 31;
            public static bool NoPlayerDamage { get; } = false;
            public static bool ManipulateAtmosphere { get; } = false;
            public static float ShipTemperature { get; } = 21f;
            public static float ShipPressure { get; } = 1f;
            public static float ShipOxygen { get; } = 1f;
            public static bool NoShipDamage { get; } = false;
            public static bool NoBreakerTrip { get; } = false;
            public static bool NoJetpackOxygenDepletion { get; } = false;
            public static bool NoBiomassDepletion { get; } = false;
            public static float BiomassMultiplier { get; } = 1f;
            public static float FabricatorSpeedMultiplier { get; } = 1f;
        }
    }
}

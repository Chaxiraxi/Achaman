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

        internal static class Defaults {
            public static bool InfiniteAbility { get; } = false;
            public static bool PrintForFree { get; } = false;
            public static bool UpgradeFabricatorForFree { get; } = false;
            public static float MultiplyRecycledAlloy { get; } = 1f;
            public static float JetpackThrustMultiplier { get; } = 1f;
            public static float JetpackDashForceMultiplier { get; } = 1f;
            public static float TotalOwnedPoints { get; set; } = 31;
            public static bool NoPlayerDamage { get; } = false;
        }
    }
}

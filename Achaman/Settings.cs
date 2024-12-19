namespace Achaman {
    internal static class Settings {
        public static bool InfiniteAbility { get; set; } = Defaults.InfiniteAbility;
        public static bool PrintForFree { get; set; } = Defaults.PrintForFree;
        public static bool UpgradeFabricatorForFree { get; set; } = Defaults.UpgradeFabricatorForFree;
        public static float MultiplyRecycledAlloy { get; set; } = Defaults.MultiplyRecycledAlloy;
        public static float JetpackThrustMultiplier { get; set; } = Defaults.JetpackThrustMultiplier;
        public static float JetpackDashForceMultiplier { get; set; } = Defaults.JetpackDashForceMultiplier;
        public static bool LearnAllRecipes { get; set; } = Defaults.LearnAllRecipes;
        public static bool NoPlayerDamage { get; set; } = Defaults.NoPlayerDamage;
        public static bool NoShipDamage { get; set; } = Defaults.NoShipDamage;
        public static bool SetHealthToMax { get; set; } = Defaults.SetHealthToMax;

        internal static class Defaults {
            public static bool InfiniteAbility { get; } = false;
            public static bool PrintForFree { get; } = false;
            public static bool UpgradeFabricatorForFree { get; } = false;
            public static float MultiplyRecycledAlloy { get; } = 1f;
            public static float JetpackThrustMultiplier { get; } = 1f;
            public static float JetpackDashForceMultiplier { get; } = 1f;
            public static bool LearnAllRecipes { get; } = false;
            public static bool NoPlayerDamage { get; } = false;
            public static bool NoShipDamage { get; } = false;
            public static bool SetHealthToMax { get; } = false;
        }
    }
}
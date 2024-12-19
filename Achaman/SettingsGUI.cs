using static UnityEngine.GUILayout;
using VoidManager.CustomGUI;

namespace Achaman {
    class SettingsGUI : ModSettingsMenu {
        public override string Name() {return PluginInfo.PLUGIN_NAME + " Settings";}

        public override void Draw() {
            Settings.InfiniteAbility = Toggle(Settings.InfiniteAbility, "Infinite Ability");
            Settings.PrintForFree = Toggle(Settings.PrintForFree, "Print For Free");
            Settings.UpgradeFabricatorForFree = Toggle(Settings.UpgradeFabricatorForFree, "Upgrade Fabricator For Free");
            Settings.LearnAllRecipes = Toggle(Settings.LearnAllRecipes, "Learn All Recipes");
            Settings.NoPlayerDamage = Toggle(Settings.NoPlayerDamage, "No Player Damage");
            Settings.NoShipDamage = Toggle(Settings.NoShipDamage, "No Ship Damage");
            Settings.SetHealthToMax = Toggle(Settings.SetHealthToMax, "Set health to max when invulnerable. (Note: This takes effect only when No Player Damage or No Ship Damage is enabled.)");

            Label("Multiply Recycled Alloy: " + Settings.MultiplyRecycledAlloy);
            Settings.MultiplyRecycledAlloy = HorizontalSlider(Settings.MultiplyRecycledAlloy, 0f, 10f);
            if (Button("Reset")) {
                Settings.MultiplyRecycledAlloy = Settings.Defaults.MultiplyRecycledAlloy;
            }

            Label("Jetpack Thrust Multiplier: " + Settings.JetpackThrustMultiplier);
            Settings.JetpackThrustMultiplier = HorizontalSlider(Settings.JetpackThrustMultiplier, 0f, 10f);
            if (Button("Reset")) {
                Settings.JetpackThrustMultiplier = Settings.Defaults.JetpackThrustMultiplier;
            }

            Label("Jetpack Dash Force Multiplier: " + Settings.JetpackDashForceMultiplier);
            Settings.JetpackDashForceMultiplier = HorizontalSlider(Settings.JetpackDashForceMultiplier, 0f, 10f);
            if (Button("Reset")) {
                Settings.JetpackDashForceMultiplier = Settings.Defaults.JetpackDashForceMultiplier;
            }
        }
    }
}
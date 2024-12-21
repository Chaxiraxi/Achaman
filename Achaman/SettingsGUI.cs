using static UnityEngine.GUILayout;
using UnityEngine;
using VoidManager.CustomGUI;

namespace Achaman {
    class SettingsGUI : ModSettingsMenu {
        public override string Name() {return PluginInfo.PLUGIN_NAME + " Settings";}

        public override void Draw() {
            Settings.InfiniteAbility = Toggle(Settings.InfiniteAbility, "Infinite Ability (client-side)");
            Settings.PrintForFree = Toggle(Settings.PrintForFree, "Print For Free (host-side)");
            Settings.UpgradeFabricatorForFree = Toggle(Settings.UpgradeFabricatorForFree, "Upgrade Fabricator For Free (host-side)");
            // Settings.LearnAllRecipes = Toggle(Settings.LearnAllRecipes, "Learn All Recipes");
            // Settings.NoPlayerDamage = Toggle(Settings.NoPlayerDamage, "No Player Damage");
            // Settings.NoShipDamage = Toggle(Settings.NoShipDamage, "No Ship Damage");
            // Settings.SetHealthToMax = Toggle(Settings.SetHealthToMax, "Set health to max when invulnerable. (Note: This takes effect only when No Player Damage or No Ship Damage is enabled.)");

            Label("Multiply Recycled Alloy (host-side): " + Settings.MultiplyRecycledAlloy);
            Settings.MultiplyRecycledAlloy = Mathf.Round(HorizontalSlider(Settings.MultiplyRecycledAlloy, 0f, 10f) * 10f) / 10f;
            if (Button("Reset")) {
                Settings.MultiplyRecycledAlloy = Settings.Defaults.MultiplyRecycledAlloy;
            }

            Label("Jetpack Thrust Multiplier: (client-side)" + Settings.JetpackThrustMultiplier);
            Settings.JetpackThrustMultiplier = Mathf.Round(HorizontalSlider(Settings.JetpackThrustMultiplier, 0f, 10f) * 10f) / 10f;
            if (Button("Reset")) {
                Settings.JetpackThrustMultiplier = Settings.Defaults.JetpackThrustMultiplier;
            }

            Label("Jetpack Dash Force Multiplier (client-side): " + Settings.JetpackDashForceMultiplier);
            Settings.JetpackDashForceMultiplier = Mathf.Round(HorizontalSlider(Settings.JetpackDashForceMultiplier, 0f, 10f) * 10f) / 10f;
            if (Button("Reset")) {
                Settings.JetpackDashForceMultiplier = Settings.Defaults.JetpackDashForceMultiplier;
            }
        }
    }
}
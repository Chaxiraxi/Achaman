using static UnityEngine.GUILayout;
using UnityEngine;
using VoidManager.CustomGUI;
using Achaman.Localization;

namespace Achaman {
    class SettingsGUI : ModSettingsMenu {
        public override string Name() {return PluginInfo.PLUGIN_NAME + " Settings";}

        public override void Draw() {
            Settings.InfiniteAbility = Toggle(Settings.InfiniteAbility, Language.Current.Get("InfiniteAbility"));
            Settings.PrintForFree = Toggle(Settings.PrintForFree, Language.Current.Get("PrintForFree"));
            Settings.UpgradeFabricatorForFree = Toggle(Settings.UpgradeFabricatorForFree, Language.Current.Get("UpgradeFabricatorForFree"));
            // Settings.LearnAllRecipes = Toggle(Settings.LearnAllRecipes, "Learn All Recipes");
            Settings.NoPlayerDamage = Toggle(Settings.NoPlayerDamage, Language.Current.Get("NoPlayerDamage"));
            Settings.NoShipDamage = Toggle(Settings.NoShipDamage, Language.Current.Get("NoShipDamage"));
            Settings.ManipulateAtmosphere = Toggle(Settings.ManipulateAtmosphere, Language.Current.Get("ManipulateAtmosphere"));

            Label(Language.Current.Get("MultiplyRecycledAlloy") + ": " + Settings.MultiplyRecycledAlloy);
            Settings.MultiplyRecycledAlloy = Mathf.Round(HorizontalSlider(Settings.MultiplyRecycledAlloy, 0f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) {
                Settings.MultiplyRecycledAlloy = Settings.Defaults.MultiplyRecycledAlloy;
            }

            Label(Language.Current.Get("JetpackThrustMultiplier") + ": " + Settings.JetpackThrustMultiplier);
            Label(Language.Current.Get("JetpackNote"));
            Settings.JetpackThrustMultiplier = Mathf.Round(HorizontalSlider(Settings.JetpackThrustMultiplier, 0f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) {
                Settings.JetpackThrustMultiplier = Settings.Defaults.JetpackThrustMultiplier;
            }

            Label(Language.Current.Get("JetpackDashForceMultiplier") + ": " + Settings.JetpackDashForceMultiplier);
            Label(Language.Current.Get("JetpackNote"));
            Settings.JetpackDashForceMultiplier = Mathf.Round(HorizontalSlider(Settings.JetpackDashForceMultiplier, 0f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) {
                Settings.JetpackDashForceMultiplier = Settings.Defaults.JetpackDashForceMultiplier;
            }

            Label(Language.Current.Get("PerkPoints") + ": " + Settings.TotalOwnedPoints);
            Settings.TotalOwnedPoints = Mathf.RoundToInt(HorizontalSlider(Settings.TotalOwnedPoints, 1f, 95f));
            if (Button(Language.Current.Get("Reset"))) {
                Settings.TotalOwnedPoints = Settings.Defaults.TotalOwnedPoints;
            }

            Label(Language.Current.Get("ShipTemperature") + ": " + Settings.ShipTemperature);
            Settings.ShipTemperature = Mathf.Round(HorizontalSlider(Settings.ShipTemperature, -273f, 501f));
            if (Button(Language.Current.Get("Reset"))) {
                Settings.ShipTemperature = Settings.Defaults.ShipTemperature;
            }

            Label(Language.Current.Get("ShipPressure") + ": " + Mathf.Round(Settings.ShipPressure * 100f) + "%");
            Settings.ShipPressure = Mathf.Round(HorizontalSlider(Settings.ShipPressure, 0f, 1f) * 100f) / 100f;
            if (Button(Language.Current.Get("Reset"))) {
                Settings.ShipPressure = Settings.Defaults.ShipPressure;
            }

            Label(Language.Current.Get("ShipOxygen") + ": " + Mathf.Round(Settings.ShipOxygen * 100f) + "%");
            Settings.ShipOxygen = Mathf.Round(HorizontalSlider(Settings.ShipOxygen, 0f, 1f) * 100f) / 100f;
            if (Button(Language.Current.Get("Reset"))) {
                Settings.ShipOxygen = Settings.Defaults.ShipOxygen;
            }

            Label(Language.Current.Get("LanguageLabel") + ": " + (Language.Current is English ? "English" : "French"));
            if (Button("English")) {
                Language.SetLanguage(new English());
            }
            if (Button("French")) {
                Language.SetLanguage(new French());
            }
        }
    }
}
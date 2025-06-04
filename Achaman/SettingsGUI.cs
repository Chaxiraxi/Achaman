using static UnityEngine.GUILayout;
using UnityEngine;
using VoidManager.CustomGUI;
using Achaman.Localization;
using VoidManager;
using System.Linq;
using Achaman.Console;  // added for Executor

namespace Achaman
{
    class SettingsGUI : ModSettingsMenu
    {
        private static bool isProgressDisabled = false;
        public override string Name() { return PluginInfo.PLUGIN_NAME + " Settings"; }
        private static string consoleInput = "";

        public override void Draw()
        {
            Label(Language.Current.Get("LanguageLabel") + ": " + (Language.Current is English ? "English" : "French"));
            if (Button("English")) Language.SetLanguage(new English());
            if (Button("French")) Language.SetLanguage(new French());

            // Disables progression to keep the game fair.
            if (!isProgressDisabled && AchamanPlugin.SHOULD_DISABLE_PROGRESS)
            {
                Label(Language.Current.Get("HostOnly"));
                if (!VoidManagerPlugin.isHosting) return;
                if (Button(Language.Current.Get("DisableProgress")))
                {
                    isProgressDisabled = true;
                    VoidManager.Progression.ProgressionHandler.DisableProgression(MyPluginInfo.PLUGIN_GUID);
                }
                return;
            }

            Settings.InfiniteAbility = Toggle(Settings.InfiniteAbility, Language.Current.Get("InfiniteAbility"));
            Settings.PrintForFree = Toggle(Settings.PrintForFree, Language.Current.Get("PrintForFree"));
            Settings.UpgradeFabricatorForFree = Toggle(Settings.UpgradeFabricatorForFree, Language.Current.Get("UpgradeFabricatorForFree"));
            Settings.NoPlayerDamage = Toggle(Settings.NoPlayerDamage, Language.Current.Get("NoPlayerDamage"));
            Settings.NoShipDamage = Toggle(Settings.NoShipDamage, Language.Current.Get("NoShipDamage"));
            Settings.ManipulateAtmosphere = Toggle(Settings.ManipulateAtmosphere, Language.Current.Get("ManipulateAtmosphere"));
            Settings.NoBreakerTrip = Toggle(Settings.NoBreakerTrip, Language.Current.Get("NoBreakerTrip"));
            Settings.NoJetpackOxygenDepletion = Toggle(Settings.NoJetpackOxygenDepletion, Language.Current.Get("NoJetpackOxygenDepletion"));
            Settings.NoBiomassDepletion = Toggle(Settings.NoBiomassDepletion, Language.Current.Get("NoBiomassDepletion"));

            Label(Language.Current.Get("MultiplyRecycledAlloy") + ": " + Settings.MultiplyRecycledAlloy);
            Settings.MultiplyRecycledAlloy = Mathf.Round(HorizontalSlider(Settings.MultiplyRecycledAlloy, 0f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) Settings.MultiplyRecycledAlloy = Settings.Defaults.MultiplyRecycledAlloy;

            Label(Language.Current.Get("BiomassMultiplier") + ": " + Settings.BiomassMultiplier);
            Settings.BiomassMultiplier = Mathf.Round(HorizontalSlider(Settings.BiomassMultiplier, 0f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) Settings.BiomassMultiplier = Settings.Defaults.BiomassMultiplier;

            Label(Language.Current.Get("JetpackThrustMultiplier") + ": " + Settings.JetpackThrustMultiplier);
            Label(Language.Current.Get("JetpackNote"));
            Settings.JetpackThrustMultiplier = Mathf.Round(HorizontalSlider(Settings.JetpackThrustMultiplier, 0f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) Settings.JetpackThrustMultiplier = Settings.Defaults.JetpackThrustMultiplier;

            Label(Language.Current.Get("JetpackDashForceMultiplier") + ": " + Settings.JetpackDashForceMultiplier);
            Label(Language.Current.Get("JetpackNote"));
            Settings.JetpackDashForceMultiplier = Mathf.Round(HorizontalSlider(Settings.JetpackDashForceMultiplier, 0f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) Settings.JetpackDashForceMultiplier = Settings.Defaults.JetpackDashForceMultiplier;

            Label(Language.Current.Get("FabricatorSpeedMultiplier") + ": " + Settings.FabricatorSpeedMultiplier);
            Settings.FabricatorSpeedMultiplier = Mathf.Round(HorizontalSlider(Settings.FabricatorSpeedMultiplier, 0.1f, 10f) * 10f) / 10f;
            if (Button(Language.Current.Get("Reset"))) Settings.FabricatorSpeedMultiplier = Settings.Defaults.FabricatorSpeedMultiplier;

            Label(Language.Current.Get("PerkPoints") + ": " + Settings.TotalOwnedPoints);
            Settings.TotalOwnedPoints = Mathf.RoundToInt(HorizontalSlider(Settings.TotalOwnedPoints, 1f, 95f));
            if (Button(Language.Current.Get("Reset"))) Settings.TotalOwnedPoints = Settings.Defaults.TotalOwnedPoints;

            Label(Language.Current.Get("ShipTemperature") + ": " + Settings.ShipTemperature);
            Settings.ShipTemperature = Mathf.Round(HorizontalSlider(Settings.ShipTemperature, -273f, 501f));
            if (Button(Language.Current.Get("Reset"))) Settings.ShipTemperature = Settings.Defaults.ShipTemperature;

            Label(Language.Current.Get("ShipPressure") + ": " + Mathf.Round(Settings.ShipPressure * 100f) + "%");
            Settings.ShipPressure = Mathf.Round(HorizontalSlider(Settings.ShipPressure, 0f, 1f) * 100f) / 100f;
            if (Button(Language.Current.Get("Reset"))) Settings.ShipPressure = Settings.Defaults.ShipPressure;

            Label(Language.Current.Get("ShipOxygen") + ": " + Mathf.Round(Settings.ShipOxygen * 100f) + "%");
            Settings.ShipOxygen = Mathf.Round(HorizontalSlider(Settings.ShipOxygen, 0f, 1f) * 100f) / 100f;
            if (Button(Language.Current.Get("Reset"))) Settings.ShipOxygen = Settings.Defaults.ShipOxygen;

            if (Button(Language.Current.Get("LearnAllBlueprints"))) Patches.BlueprintManager.LearnAllSavedBlueprints();

            Label(Language.Current.Get("DebugConsole"));
            GUI.SetNextControlName("ConsoleInput");
            consoleInput = TextField(consoleInput);

            var e = Event.current;
            if (GUI.GetNameOfFocusedControl() == "ConsoleInput" && e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.UpArrow)
                {
                    consoleInput = Executor.PreviousHistory();
                    e.Use();
                }
                else if (e.keyCode == KeyCode.DownArrow)
                {
                    consoleInput = Executor.NextHistory();
                    e.Use();
                }
                else if (e.keyCode == KeyCode.Tab)
                {
                    consoleInput = Executor.GetAutoComplete(consoleInput);
                    e.Use();
                }
                else if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)
                {
                    string[] parts = consoleInput.Split(' ');
                    string command = parts[0];
                    string[] args = parts.Skip(1).ToArray();
                    Executor.ExecuteCommand(command, args);
                    consoleInput = "";
                    e.Use();
                }
            }

            if (Button(Language.Current.Get("ExecuteCommand")))
            {
                string[] parts = consoleInput.Split(' ');
                string command = parts[0];
                string[] args = parts.Skip(1).ToArray();
                Console.Executor.ExecuteCommand(command, args);
                consoleInput = "";
            }

            Label(Language.Current.Get("ResetAll"));
            if (Button(Language.Current.Get("Reset"))) Settings.Reset();
        }
    }
}
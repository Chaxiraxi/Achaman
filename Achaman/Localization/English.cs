using System.Collections.Generic;

namespace Achaman.Localization
{
    public class English : ILanguage
    {
        private readonly Dictionary<string, string> translations = new Dictionary<string, string> {
            { "InfiniteAbility", "Infinite Ability (client-side)" },
            { "PrintForFree", "Print For Free (host-side)" },
            { "UpgradeFabricatorForFree", "Upgrade Fabricator For Free (host-side)" },
            { "MultiplyRecycledAlloy", "Multiply Recycled Alloy (host-side)" },
            { "JetpackThrustMultiplier", "Jetpack Thrust Multiplier (client-side)" },
            { "JetpackDashForceMultiplier", "Jetpack Dash Force Multiplier (client-side)" },
            { "JetpackNote", "Note: This must be configured in the Hub to take effect." },
            { "PerkPoints", "Perk points (client-side)" },
            { "Reset", "Reset" },
            // { "LanguageLabel", "Language" },
            { "NoPlayerDamage", "No player damage (client-side)" },
            { "ManipulateAtmosphere", "Manipulate atmosphere (host-side)" },
            { "ShipTemperature", "Ship Temperature (host-side)" },
            { "ShipPressure", "Ship Pressure (host-side)" },
            { "ShipOxygen", "Ship Oxygen (host-side)" },
            { "NoShipDamage", "No Ship Damage (host-side)" },
            { "NoBreakerTrip", "No Breaker Trip (host-side)" },
            { "NoJetpackOxygenDepletion", "Prevent Jetpack Oxygen Depletion (client-side)" },
            { "NoBiomassDepletion", "Prevent Biomass Depletion (host-side)" },
            { "BiomassMultiplier", "Biomass Multiplier (host-side)" },
            { "LearnAllBlueprints", "Learn All Blueprints" },
            { "ResetAll", "Reset all settings" },
            { "FabricatorSpeedMultiplier", "Fabricator Speed Multiplier (host-side)" },
            { "HostOnly", "This mod works only when progression is disabled when you're hosting. Keep the game fair :D" },
            { "DisableProgress", "Disable Progress" },
            { "DebugConsole", "Debug Console (Still in beta state)" },
            { "ExecuteCommand", "Execute Command" },
            { "RemoveMutatorLimit", "Remove Mutator Limit (host-side)" },
            { "ConsoleToggleKeyLabel", "Console Toggle Key" },
            { "PressAnyKeyLabel", "Press any key..." },
            { "ChangeKeyButtonLabel", "Change Key" },
            { "SpoofVanillaRoom", "Spoof the current session as being vanilla (host-side)" }
        };

        public string Get(string key)
        {
            if (translations.TryGetValue(key, out string value))
            {
                return value;
            }
            return key;
        }
    }
}
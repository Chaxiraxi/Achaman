using System.Collections.Generic;

namespace Achaman.Localization
{
    public class German : ILanguage
    {
        private readonly Dictionary<string, string> translations = new Dictionary<string, string> {
            { "InfiniteAbility", "Unendliche Fähigkeit (Client-seitig)" },
            { "PrintForFree", "Kostenlos drucken (Host-seitig)" },
            { "UpgradeFabricatorForFree", "Fabrikator kostenlos upgraden (Host-seitig)" },
            { "MultiplyRecycledAlloy", "Recycelte Legierung multiplizieren (Host-seitig)" },
            { "JetpackThrustMultiplier", "Jetpack Schub-Multiplikator (Client-seitig)" },
            { "JetpackDashForceMultiplier", "Jetpack Dash-Kraft-Multiplikator (Client-seitig)" },
            { "JetpackNote", "Hinweis: Dies muss im Hub konfiguriert werden, um wirksam zu werden." },
            { "PerkPoints", "Fertigkeitspunkte (Client-seitig)" },
            { "Reset", "Zurücksetzen" },
            // { "LanguageLabel", "Sprache" },
            { "NoPlayerDamage", "Kein Spielerschaden (Client-seitig)" },
            { "ManipulateAtmosphere", "Atmosphäre manipulieren (Host-seitig)" },
            { "ShipTemperature", "Schiffstemperatur (Host-seitig)" },
            { "ShipPressure", "Schiffsdruck (Host-seitig)" },
            { "ShipOxygen", "Schiffssauerstoff (Host-seitig)" },
            { "NoShipDamage", "Kein Schiffsschaden (Host-seitig)" },
            { "NoBreakerTrip", "Keine Sicherungsauslösung (Host-seitig)" },
            { "NoJetpackOxygenDepletion", "Jetpack Sauerstoffverbrauch verhindern (Client-seitig)" },
            { "NoBiomassDepletion", "Biomasse-Verbrauch verhindern (Host-seitig)" },
            { "BiomassMultiplier", "Biomasse-Multiplikator (Host-seitig)" },
            { "LearnAllBlueprints", "Alle Baupläne lernen" },
            { "ResetAll", "Alle Einstellungen zurücksetzen" },
            { "FabricatorSpeedMultiplier", "Fabrikator Geschwindigkeits-Multiplikator (Host-seitig)" },
            { "HostOnly", "Diese Mod funktioniert nur, wenn der Fortschritt deaktiviert ist, wenn Sie der Host sind. Halten Sie das Spiel fair :D" },
            { "DisableProgress", "Fortschritt deaktivieren" },
            { "DebugConsole", "Debug-Konsole (Noch im Beta-Zustand)" },
            { "ExecuteCommand", "Befehl ausführen" },
            { "RemoveMutatorLimit", "Mutator-Limit entfernen (Host-seitig)" },
            { "ConsoleToggleKeyLabel", "Konsole Umschalttaste" },
            { "PressAnyKeyLabel", "Beliebige Taste drücken..." },
            { "ChangeKeyButtonLabel", "Taste ändern" }
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
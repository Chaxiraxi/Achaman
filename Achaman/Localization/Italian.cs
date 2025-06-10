using System.Collections.Generic;

namespace Achaman.Localization
{
    public class Italian : ILanguage
    {
        private readonly Dictionary<string, string> translations = new Dictionary<string, string> {
            { "InfiniteAbility", "Abilità infinita (lato client)" },
            { "PrintForFree", "Stampa gratis (lato host)" },
            { "UpgradeFabricatorForFree", "Aggiorna Fabbricatore gratis (lato host)" },
            { "MultiplyRecycledAlloy", "Moltiplica lega riciclata (lato host)" },
            { "JetpackThrustMultiplier", "Moltiplicatore spinta Jetpack (lato client)" },
            { "JetpackDashForceMultiplier", "Moltiplicatore forza scatto Jetpack (lato client)" },
            { "JetpackNote", "Nota: Questo deve essere configurato nell'Hub per avere effetto." },
            { "PerkPoints", "Punti abilità (lato client)" },
            { "Reset", "Ripristina" },
            // { "LanguageLabel", "Lingua" },
            { "NoPlayerDamage", "Nessun danno giocatore (lato client)" },
            { "ManipulateAtmosphere", "Manipola atmosfera (lato host)" },
            { "ShipTemperature", "Temperatura nave (lato host)" },
            { "ShipPressure", "Pressione nave (lato host)" },
            { "ShipOxygen", "Ossigeno nave (lato host)" },
            { "NoShipDamage", "Nessun danno nave (lato host)" },
            { "NoBreakerTrip", "Nessun scatto interruttore (lato host)" },
            { "NoJetpackOxygenDepletion", "Previeni consumo ossigeno Jetpack (lato client)" },
            { "NoBiomassDepletion", "Previeni consumo biomassa (lato host)" },
            { "BiomassMultiplier", "Moltiplicatore biomassa (lato host)" },
            { "LearnAllBlueprints", "Impara tutti i progetti" },
            { "ResetAll", "Ripristina tutte le impostazioni" },
            { "FabricatorSpeedMultiplier", "Moltiplicatore velocità Fabbricatore (lato host)" },
            { "HostOnly", "Questa mod funziona solo quando la progressione è disabilitata quando sei l'host. Mantieni il gioco equo :D" },
            { "DisableProgress", "Disabilita progressione" },
            { "DebugConsole", "Console di debug (Ancora in stato beta)" },
            { "ExecuteCommand", "Esegui comando" },
            { "RemoveMutatorLimit", "Rimuovi limite mutatori (lato host)" },
            { "ConsoleToggleKeyLabel", "Tasto attivazione console" },
            { "PressAnyKeyLabel", "Premi un tasto qualsiasi..." },
            { "ChangeKeyButtonLabel", "Cambia tasto" },
            { "SpoofVanillaRoom", "Simula la sessione corrente come vanilla (lato host)" }
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
using System.Collections.Generic;

namespace Achaman.Localization {
    public class French : ILanguage {
        private readonly Dictionary<string, string> translations = new Dictionary<string, string> {
            { "InfiniteAbility", "Capacité infinie (côté client)" },
            { "PrintForFree", "Imprimer gratuitement (côté hôte)" },
            { "UpgradeFabricatorForFree", "Améliorer le Fabricator gratuitement (côté hôte)" },
            { "MultiplyRecycledAlloy", "Multiplier l'alliage recyclé (côté hôte)" },
            { "JetpackThrustMultiplier", "Multiplicateur de poussée Jetpack (côté client)" },
            { "JetpackDashForceMultiplier", "Multiplicateur de dash Jetpack (côté client)" },
            { "JetpackNote", "Note: Cela doit être configuré dans le Hub pour prendre effet." },
            { "PerkPoints", "Points de compétence (côté client)" },
            { "Reset", "Réinitialiser" },
            { "LanguageLabel", "Langue" },
            { "NoPlayerDamage", "Pas de dégâts de joueur (host-side)" },
            { "ManipulateAtmosphere", "Manipuler l'atmosphère (côté hôte)" },
            { "ShipTemperature", "Température du vaisseau (côté hôte)" },
            { "ShipPressure", "Pression du vaisseau (côté hôte)" },
            { "ShipOxygen", "Oxygène du vaisseau (côté hôte)" }
        };

        public string Get(string key) {
            if (translations.TryGetValue(key, out string value)) {
                return value;
            }
            return key;
        }
    }
}
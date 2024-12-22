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
            { "PerkPoints", "Points de compétence" },
            { "Reset", "Réinitialiser" },
            { "LanguageLabel", "Langue" }
        };

        public string Get(string key) {
            if (translations.TryGetValue(key, out string value)) {
                return value;
            }
            return key;
        }
    }
}
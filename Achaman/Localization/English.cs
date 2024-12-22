using System.Collections.Generic;

namespace Achaman.Localization {
    public class English : ILanguage {
        private readonly Dictionary<string, string> translations = new Dictionary<string, string> {
            { "InfiniteAbility", "Infinite Ability (client-side)" },
            { "PrintForFree", "Print For Free (host-side)" },
            { "UpgradeFabricatorForFree", "Upgrade Fabricator For Free (host-side)" },
            { "MultiplyRecycledAlloy", "Multiply Recycled Alloy (host-side)" },
            { "JetpackThrustMultiplier", "Jetpack Thrust Multiplier (client-side)" },
            { "JetpackDashForceMultiplier", "Jetpack Dash Force Multiplier (client-side)" },
            { "PerkPoints", "Perk points" },
            { "Reset", "Reset" },
            { "LanguageLabel", "Language" }
        };

        public string Get(string key) {
            if (translations.TryGetValue(key, out string value)) {
                return value;
            }
            return key;
        }
    }
}
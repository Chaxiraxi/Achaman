
namespace Achaman.Localization {
    public interface ILanguage {
        string Get(string key);
    }

    public static class Language {
        private static ILanguage current = new English();
        public static ILanguage Current => current;

        public static void SetLanguage(ILanguage language) {
            current = language;
        }
    }
}
using System.Linq;
using System.Collections.Generic;

namespace AdvancedGamePad.Model
{
    public class Language
    {
        private Dictionary<string, Dictionary<string, string>> _languages = null;

        public Dictionary<string, Dictionary<string, string>> GetLocalizations()
        {
            return new Dictionary<string, Dictionary<string, string>>(_languages);
        }

        public Language(Dictionary<string, string> localization)
        {
            _languages = new Dictionary<string, Dictionary<string, string>>();
            _languages.Add(localization["Locale"], localization);
        }

        public void AddLocalization(Dictionary<string, string> localization)
        {
            if (localization != null && IsValid(localization))
            {
                _languages.Add(localization["Locale"], localization);
            }
        }

        private bool IsValid(Dictionary<string, string> localization)
        {
            return _languages["English"].Keys.All(localization.Keys.Contains);
        }
    }
}
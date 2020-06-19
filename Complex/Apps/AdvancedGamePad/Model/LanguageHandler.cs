using System;
using System.Collections.Generic;
using System.IO;

using AdvancedGamePad.Core;

namespace AdvancedGamePad.Model
{
    public class LanguageHandler : IFileHandler<Language>
    {
        public LanguageHandler() { }

        public Language GetDefault()
        {
            Language lang = new Language(new Dictionary<string, string>(Factory.Resources.EnglishLocale));
            return lang;
        }

        public Language Load(string path)
        {
            Language lang = GetDefault();
            if (Directory.Exists(path))
            {
                string[] locList = Directory.GetFiles(path, "*.locale");
                foreach (var locPath in locList)
                {
                    var localization = Read(locPath);
                    if (localization != null)
                        lang.AddLocalization(localization);
                }
            }
            return lang;
        }

        private Dictionary<String, String> Read(string path)
        {
            if (!File.Exists(path))
                return null;
            try
            {
                Dictionary<String, String> locale = new Dictionary<String, String>();
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        String[] property = line.Split('=');
                        if (line.Equals(String.Empty) || property.Length < 2)
                            continue;

                        locale.Add(property[0], property[1]);
                    }
                }
                if (locale.Count == 0)
                    return null;
                return locale;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public bool Save(Language item, string filePath)
        {
            return false;
        }
    }
}

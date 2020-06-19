using System;
using System.Collections.Generic;
using System.IO;

using AdvancedGamePad.Core;

namespace AdvancedGamePad.Model
{
    public class SettingsHandler : IFileHandler<CommonSettings>
    {
        public SettingsHandler() { }

        public CommonSettings GetDefault()
        {
            CommonSettings settings = new CommonSettings();
            
            settings.AutoLaunchOnSystemStartUp = false;
            settings.StartAppMinimized = false;
            settings.AutoStart = false;
            settings.Language = "English";
            settings.DefaultProfile = "default.profile";

            return settings;
        }

        public CommonSettings Load(string filePath)
        {
            CommonSettings settings = new CommonSettings();
            try
            {
                using (StreamReader fs = new StreamReader(filePath))
                {
                    var properties = new Dictionary<String, String>();

                    string line;
                    while ((line = fs.ReadLine()) != null)
                    {
                        string[] prop = line.Split('=');
                        prop[0] = prop[0].Replace(" ", String.Empty);
                        if (!prop[0].Equals("DefaultProfile"))
                            prop[1] = prop[1].Replace(" ", String.Empty);
                        properties.Add(prop[0], prop[1]);
                    }
                    
                    if (properties.TryGetValue(nameof(settings.AutoLaunchOnSystemStartUp), out line))
                    {
                        settings.AutoLaunchOnSystemStartUp = Parse.Booleans(line);
                    }
                    if (properties.TryGetValue(nameof(settings.StartAppMinimized), out line))
                    {
                        settings.StartAppMinimized = Parse.Booleans(line);
                    }
                    if (properties.TryGetValue(nameof(settings.AutoStart), out line))
                    {
                        settings.AutoStart = Parse.Booleans(line);
                    }
                    if (properties.TryGetValue(nameof(Language), out line))
                    {
                        settings.Language = line;
                    }
                    if (properties.TryGetValue(nameof(settings.DefaultProfile), out line))
                    {
                        settings.DefaultProfile = line;
                    }
                }

                return settings;
            }
            catch (IOException)
            {
                Console.WriteLine("Cannot load settings. Default settings file will be created.");
                return null;
            }
        }

        public bool Save(CommonSettings settings, string filePath)
        {
            try
            {
                using (StreamWriter fs = new StreamWriter(filePath, false))
                {
                    fs.WriteLine(nameof(settings.AutoLaunchOnSystemStartUp) + "=" + settings.AutoLaunchOnSystemStartUp);
                    fs.WriteLine(nameof(settings.StartAppMinimized) + "=" + settings.StartAppMinimized);
                    fs.WriteLine(nameof(settings.AutoStart) + "=" + settings.AutoStart);
                    fs.WriteLine(nameof(settings.Language) + "=" + settings.Language);
                    fs.WriteLine(nameof(settings.DefaultProfile) + "=" + settings.DefaultProfile);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
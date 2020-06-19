using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

using SpaceVIL;

using AdvancedGamePad.Core;

namespace AdvancedGamePad.Model
{
    public delegate void EventValueChange(string name);

    public sealed class ConfigManager
    {
        #region events
        public EventIOFails EventOnIOProfileFail = null;
        public EventIOFails EventOnIOLanguageFail = null;
        public EventIOFails EventOnIOSettingsFail = null;
        public EventValueChange EventOnLanguageChange = null;
        public EventValueChange EventOnProfileChange = null;
        public EventCommonMethod EventOnSettingsChange = null;
        #endregion

        #region  common settings
        private IFileHandler<CommonSettings> _settingsHandler = null;
        public CommonSettings CommonSettings
        {
            get;
            private set;
        }
        #endregion

        #region profile
        private IFileHandler<Profile> _profileHandler = null;
        public Dictionary<String, Profile> ProfileList
        {
            get;
            private set;
        }
        public Profile CurrentProfile
        {
            get;
            set;
        }
        #endregion

        #region  localizations
        private IFileHandler<Language> _languageHandler = null;
        public Language Languages
        {
            get;
            private set;
        }
        public Dictionary<string, string> CurrentLanguage
        {
            get;
            private set;
        }
        #endregion

        public ConfigManager()
        {
            // app settings
            _settingsHandler = new SettingsHandler();
            CommonSettings = _settingsHandler.Load(AppDomain.CurrentDomain.BaseDirectory + "settings.cfg");
            if (CommonSettings == null)
            {
                CommonSettings = _settingsHandler.GetDefault();
                _settingsHandler.Save(CommonSettings, AppDomain.CurrentDomain.BaseDirectory + "settings.cfg");
            }

            // profile
            _profileHandler = new ProfileHandler();
            ProfileList = new Dictionary<String, Profile>();
            if (!LoadProfiles())
            {
                CurrentProfile = _profileHandler.GetDefault();
                ProfileList.Add(CurrentProfile.Name, CurrentProfile);
                SaveProfile();
            }
            else
            {
                if (ProfileList.TryGetValue(CommonSettings.DefaultProfile, out Profile profile))
                {
                    CurrentProfile = profile;
                }
                else
                {
                    CurrentProfile = ProfileList.Values.OrderBy(p => p.Name).ToList().ElementAt(0);
                    CommonSettings.DefaultProfile = CurrentProfile.Name;
                    SaveSettings();
                }
            }

            // app language
            _languageHandler = new LanguageHandler();
            Languages = _languageHandler.Load(AppDomain.CurrentDomain.BaseDirectory + "locale\\");
            CurrentLanguage = Languages.GetLocalizations().TryGetValue(CommonSettings.Language, out var loc)
                    ? loc
                    : Languages.GetLocalizations()["English"];

            EventOnLanguageChange = (name) =>
            {
                CurrentLanguage = Languages.GetLocalizations().TryGetValue(name, out loc)
                    ? loc
                    : Languages.GetLocalizations()["English"];
                CommonSettings.Language = CurrentLanguage["Locale"];
            };
        }

        public bool AddProfile(string name)
        {
            Profile profile = _profileHandler.GetDefault();
            profile.Name = name;

            if (ProfileList.ContainsKey(profile.Name))
                return false;

            ProfileList.Add(profile.Name, profile);

            if (!_profileHandler.Save(profile, AppDomain.CurrentDomain.BaseDirectory + "profiles\\" + profile.Name))
            {
                EventOnIOProfileFail.Invoke("Cannot save profile file: <" + profile.Name + ">",
                    "Load profile exception");
                return false;
            }

            return true;
        }

        public bool CopyProfile(string name)
        {
            Profile profile = (Profile)CurrentProfile.Clone();
            profile.Name = name;

            if (ProfileList.ContainsKey(profile.Name))
                return false;

            ProfileList.Add(profile.Name, profile);

            if (!_profileHandler.Save(profile, AppDomain.CurrentDomain.BaseDirectory + "profiles\\" + profile.Name))
            {
                EventOnIOProfileFail.Invoke("Cannot save profile file: <" + profile.Name + ">",
                    "Load profile exception");
                return false;
            }

            return true;
        }

        public bool DeleteProfile(Profile profile)
        {
            if (ProfileList.Count > 1)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "profiles\\" + profile.Name))
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "profiles\\" + profile.Name);

                return ProfileList.Remove(profile.Name);
            }

            return false;
        }

        private bool LoadProfiles()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "profiles\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return false;
            }

            string[] files = Directory.GetFiles(path);
            if (files.Length == 0)
            {
                return false;
            }
            foreach (string file in files)
            {
                Profile p = _profileHandler.Load(file);
                if (p != null)
                {
                    ProfileList.Add(p.Name, p);
                }
                else
                {
                    EventOnIOProfileFail.Invoke("Cannot load profile file: <" + file + ">",
                        "Load profile exception");
                }
            }
            return true;
        }

        public bool SaveProfile()
        {
            if (!_profileHandler.Save(CurrentProfile, AppDomain.CurrentDomain.BaseDirectory + "profiles\\" + CurrentProfile.Name))
            {
                EventOnIOProfileFail.Invoke("Cannot save profile file: <" + CurrentProfile.Name + ">",
                    "Load profile exception");
                return false;
            }
            return true;
        }

        public void ApplyProfile(Profile profile)
        {
            if (profile != null)
            {
                CurrentProfile = profile;
            }
        }

        public bool SaveSettings()
        {
            if (!_settingsHandler.Save(CommonSettings, AppDomain.CurrentDomain.BaseDirectory + "settings.cfg"))
            {
                EventOnIOSettingsFail.Invoke("Cannot save settings file: <settings.cfg>",
                    "Load profile exception");
                return false;
            }
            if(CommonSettings.AutoLaunchOnSystemStartUp)
            {
                AddToStartup();
            }
            else
            {
                RemoveFromStartup();
            }
            return true;
        }

        public void AddToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("AdvancedGamePad",
                    "\"" + AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName + "\"");
            }
        }

        public void RemoveFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("AdvancedGamePad", false);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AdvancedGamePad.Core;

namespace AdvancedGamePad.Model
{
    public class ProfileHandler : IFileHandler<Profile>
    {
        public ProfileHandler()
        {

        }

        public Profile Load(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            try
            {
                Dictionary<String, String> fileStrings = new Dictionary<String, String>();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        String[] property = line.Split('=');
                        if (line.Equals(String.Empty) || property.Length < 2)
                            continue;

                        fileStrings.Add(property[0], property[1]);
                    }
                }
                if (fileStrings.Count == 0)
                    return null;

                Profile profile = new Profile();

                String value;
                profile.Name = fileStrings.TryGetValue("Name", out value) ? value : "default.profile";
                profile.ButtonA = fileStrings.TryGetValue("ButtonA", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Enter };
                profile.ButtonX = fileStrings.TryGetValue("ButtonX", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Menu };
                profile.ButtonY = fileStrings.TryGetValue("ButtonY", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Space };
                profile.ButtonB = fileStrings.TryGetValue("ButtonB", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Backspace };
                profile.ButtonDUp = fileStrings.TryGetValue("ButtonDUp", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Up };
                profile.ButtonDDown = fileStrings.TryGetValue("ButtonDDown", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Down };
                profile.ButtonDLeft = fileStrings.TryGetValue("ButtonDLeft", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Left };
                profile.ButtonDRight = fileStrings.TryGetValue("ButtonDRight", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Right };
                profile.ButtonHome = fileStrings.TryGetValue("ButtonHome", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.F1 };
                profile.ButtonBack = fileStrings.TryGetValue("ButtonBack", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.End };
                profile.ButtonStart = fileStrings.TryGetValue("ButtonStart", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.Home };
                profile.ButtonRT = fileStrings.TryGetValue("ButtonRT", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.LeftShift };
                profile.ButtonRB = fileStrings.TryGetValue("ButtonRB", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.LeftAlt };
                profile.ButtonLT = fileStrings.TryGetValue("ButtonLT", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.LeftSuper };
                profile.ButtonLB = fileStrings.TryGetValue("ButtonLB", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.LeftControl };
                profile.ButtonRS = fileStrings.TryGetValue("ButtonRS", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.PageDown };
                profile.ButtonLS = fileStrings.TryGetValue("ButtonLS", out value) ? Parse.KeyCodes(value) : new HardwareButtons[] { HardwareButtons.PageUp };
                profile.LeftThumbStickAcceleration = fileStrings.TryGetValue("LeftThumbStickAcceleration", out value) ? Parse.Booleans(value) : true;
                profile.RightThumbStickAcceleration = fileStrings.TryGetValue("RightThumbStickAcceleration", out value) ? Parse.Booleans(value) : false;
                profile.LeftThumbStickSpeed = fileStrings.TryGetValue("LeftThumbStickSpeed", out value) ? Parse.UIntegers(value) : 3;
                profile.RightThumbStickSpeed = fileStrings.TryGetValue("RightThumbStickSpeed", out value) ? Parse.UIntegers(value) : 2;
                profile.LeftStickAction = fileStrings.TryGetValue("LeftStickAction", out value) ? Parse.StickActionTypes(value) : StickActionType.MouseMove;
                profile.RightStickAction = fileStrings.TryGetValue("RightStickAction", out value) ? Parse.StickActionTypes(value) : StickActionType.MouseMove;
                profile.RepeatMode = fileStrings.TryGetValue("RepeatMode", out value) ? Parse.RepeatTypes(value) : RepeatType.AsDisplayFrequence;

                return profile;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public bool Save(Profile profile, string filePath)
        {
            StringBuilder sb = new StringBuilder("");
            try
            {
                using (StreamWriter sr = new StreamWriter(filePath))
                {
                    sb.Append("Name=" + profile.Name + "\n");
                    sb.Append("ButtonA=" + Parse.KeysToString(profile.ButtonA) + "\n");
                    sb.Append("ButtonX=" + Parse.KeysToString(profile.ButtonX) + "\n");
                    sb.Append("ButtonY=" + Parse.KeysToString(profile.ButtonY) + "\n");
                    sb.Append("ButtonB=" + Parse.KeysToString(profile.ButtonB) + "\n");
                    sb.Append("ButtonDUp=" + Parse.KeysToString(profile.ButtonDUp) + "\n");
                    sb.Append("ButtonDDown=" + Parse.KeysToString(profile.ButtonDDown) + "\n");
                    sb.Append("ButtonDLeft=" + Parse.KeysToString(profile.ButtonDLeft) + "\n");
                    sb.Append("ButtonDRight=" + Parse.KeysToString(profile.ButtonDRight) + "\n");
                    sb.Append("ButtonHome=" + Parse.KeysToString(profile.ButtonHome) + "\n");
                    sb.Append("ButtonBack=" + Parse.KeysToString(profile.ButtonBack) + "\n");
                    sb.Append("ButtonStart=" + Parse.KeysToString(profile.ButtonStart) + "\n");
                    sb.Append("ButtonRT=" + Parse.KeysToString(profile.ButtonRT) + "\n");
                    sb.Append("ButtonRB=" + Parse.KeysToString(profile.ButtonRB) + "\n");
                    sb.Append("ButtonLT=" + Parse.KeysToString(profile.ButtonLT) + "\n");
                    sb.Append("ButtonLB=" + Parse.KeysToString(profile.ButtonLB) + "\n");
                    sb.Append("ButtonRS=" + Parse.KeysToString(profile.ButtonRS) + "\n");
                    sb.Append("ButtonLS=" + Parse.KeysToString(profile.ButtonLS) + "\n");
                    sb.Append("LeftThumbStickAcceleration=" + profile.LeftThumbStickAcceleration + "\n");
                    sb.Append("RightThumbStickAcceleration=" + profile.RightThumbStickAcceleration + "\n");
                    sb.Append("LeftThumbStickSpeed=" + profile.LeftThumbStickSpeed + "\n");
                    sb.Append("RightThumbStickSpeed=" + profile.RightThumbStickSpeed + "\n");
                    sb.Append("LeftStickAction=" + profile.LeftStickAction + "\n");
                    sb.Append("RightStickAction=" + profile.RightStickAction + "\n");
                    sb.Append("RepeatMode=" + profile.RepeatMode + "\n");

                    if (sb.Equals(""))
                        return false;

                    sr.Write(sb.ToString());
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public Profile GetDefault()
        {
            Profile profile = new Profile();
            profile.Name = "default.profile";

            profile.ButtonA = new HardwareButtons[] { HardwareButtons.Enter };
            profile.ButtonX = new HardwareButtons[] { HardwareButtons.Menu };
            profile.ButtonY = new HardwareButtons[] { HardwareButtons.Space };
            profile.ButtonB = new HardwareButtons[] { HardwareButtons.Backspace };
            profile.ButtonDUp = new HardwareButtons[] { HardwareButtons.Up };
            profile.ButtonDDown = new HardwareButtons[] { HardwareButtons.Down };
            profile.ButtonDLeft = new HardwareButtons[] { HardwareButtons.Left };
            profile.ButtonDRight = new HardwareButtons[] { HardwareButtons.Right };
            profile.ButtonHome = new HardwareButtons[] { HardwareButtons.F1 };
            profile.ButtonBack = new HardwareButtons[] { HardwareButtons.End };
            profile.ButtonStart = new HardwareButtons[] { HardwareButtons.Home };
            profile.ButtonRT = new HardwareButtons[] { HardwareButtons.LeftShift };
            profile.ButtonRB = new HardwareButtons[] { HardwareButtons.LeftAlt };
            profile.ButtonLT = new HardwareButtons[] { HardwareButtons.LeftSuper };
            profile.ButtonLB = new HardwareButtons[] { HardwareButtons.LeftControl };
            profile.ButtonRS = new HardwareButtons[] { HardwareButtons.PageDown };
            profile.ButtonLS = new HardwareButtons[] { HardwareButtons.PageUp };
            profile.LeftThumbStickAcceleration = true;
            profile.RightThumbStickAcceleration = false;
            profile.LeftThumbStickSpeed = 3;
            profile.RightThumbStickSpeed = 2;
            profile.LeftStickAction = StickActionType.MouseMove;
            profile.RightStickAction = StickActionType.MouseMove;
            profile.RepeatMode = RepeatType.AsDisplayFrequence;

            return profile;
        }
    }
}
using System;
using System.Text;

using AdvancedGamePad.Core;
using AdvancedGamePad.Wrappers;

using SpaceVIL.Core;

namespace AdvancedGamePad.Model
{
    public static class Parse
    {
        public static int RepeatTypes(RepeatType type)
        {
            return (type.Equals(RepeatType.NoRepeats))
                    ? 0
                    : (type.Equals(RepeatType.AsDisplayFrequence))
                        ? 1
                        : 2;
        }

        public static HardwareButtons[] KeyCodes(string value)
        {
            string[] keysLine = Keystroke(value);
            HardwareButtons[] keySequence = new HardwareButtons[keysLine.Length];
            for (int i = 0; i < keysLine.Length; i++)
            {
                keySequence[i] = Enum.TryParse(keysLine[i], out HardwareButtons code) ? code : HardwareButtons.Unknown;
            }
            return keySequence;
        }

        public static bool Booleans(string value)
        {
            return Boolean.TryParse(value, out bool tmp) ? tmp : false;
        }

        public static uint UIntegers(string value)
        {
            return UInt32.TryParse(value, out uint tmp) ? tmp : 1;
        }

        public static StickActionType StickActionTypes(string value)
        {
            return Enum.TryParse(value, out StickActionType tmp) ? tmp : StickActionType.MouseMove;
        }

        public static int StickActionTypes(StickActionType type)
        {
            return (type.Equals(StickActionType.MouseMove)) ? 0 : 1;
        }

        public static RepeatType RepeatTypes(string value)
        {
            return Enum.TryParse(value, out RepeatType tmp) ? tmp : RepeatType.AsDisplayFrequence;
        }

        public static string[] Keystroke(string keystroke)
        {
            StringBuilder sb = new StringBuilder(keystroke);
            sb.Replace("\n", String.Empty);
            sb.Replace(" ", String.Empty);
            string[] stroke = sb.ToString().Split('+');
            for (int i = 0; i < stroke.Length; i++)
            {
                if (stroke[i].Equals("Shift") || stroke[i].Equals("Alt"))
                {
                    stroke[i] = "Left" + stroke[i];
                }
                else if (stroke[i].Equals("Win"))
                {
                    stroke[i] = "LeftSuper";
                }
                else if (stroke[i].Equals("Control"))
                {
                    stroke[i] = "LeftControl";
                }
                else if (stroke[i].Length == 1)
                {
                    if (Int32.TryParse(stroke[i], out int value))
                    {
                        stroke[i] = "Alpha" + stroke[i];
                    }
                }
            }
            return stroke;
        }

        public static string KeysToString(Profile profile, XInputWrapper.GP button)
        {
            HardwareButtons[] sequence;
            switch (button)
            {
                case XInputWrapper.GP.XINPUT_GAMEPAD_A:
                    sequence = profile.ButtonA;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_X:
                    sequence = profile.ButtonX;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_Y:
                    sequence = profile.ButtonY;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_B:
                    sequence = profile.ButtonB;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_DPAD_UP:
                    sequence = profile.ButtonDUp;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_DPAD_DOWN:
                    sequence = profile.ButtonDDown;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_DPAD_LEFT:
                    sequence = profile.ButtonDLeft;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_DPAD_RIGHT:
                    sequence = profile.ButtonDRight;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_BACK:
                    sequence = profile.ButtonBack;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_START:
                    sequence = profile.ButtonStart;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_LEFT_SHOULDER:
                    sequence = profile.ButtonLB;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_LEFT_THUMB:
                    sequence = profile.ButtonLS;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_SHOULDER:
                    sequence = profile.ButtonRB;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_THUMB:
                    sequence = profile.ButtonRS;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_TRIGGER:
                    sequence = profile.ButtonRT;
                    break;
                case XInputWrapper.GP.XINPUT_GAMEPAD_LEFT_TRIGGER:
                    sequence = profile.ButtonLT;
                    break;
                default:
                    sequence = new HardwareButtons[] { HardwareButtons.Unknown };
                    break;
            }

            return KeysToString(sequence);
        }

        public static string KeysToString(HardwareButtons[] stroke)
        {
            StringBuilder result = new StringBuilder("");
            foreach (var key in stroke)
            {
                if ((int)key >= 0 && (int)key < 3)
                    result.Append(key.ToString());
                else
                    result.Append(ToPure(key.ToString()));

                result.Append("+");
            }
            result.Remove(result.Length - 1, 1);

            return result.ToString();
        }

        public static string ToPure(string key)
        {
            if (key.Contains("Left") && key.Length > "Left".Length)
                key = key.Replace("Left", String.Empty);

            if (key.Contains("Right") && key.Length > "Right".Length)
                key = key.Replace("Right", String.Empty);

            if (key.Contains("Alpha") && key.Length > "Alpha".Length)
                key = key.Replace("Alpha", String.Empty);

            if (key.Contains("Super"))
                key = "Win";

            return key;
        }

        public static string KeysString(HardwareButtons button)
        {
            string vkey = String.Empty;

            switch (button)
            {
                case HardwareButtons.LeftShift:
                case HardwareButtons.RightShift:
                    vkey = "Shift";
                    break;
                case HardwareButtons.LeftControl:
                case HardwareButtons.RightControl:
                    vkey = "Control";
                    break;
                case HardwareButtons.LeftAlt:
                case HardwareButtons.RightAlt:
                    vkey = "Alt";
                    break;
                case HardwareButtons.LeftSuper:
                case HardwareButtons.RightSuper:
                    vkey = "Win";
                    break;
                default:
                    break;
            }

            if (vkey == String.Empty)
                vkey = button.ToString();

            return vkey.Contains("Alpha") ? vkey.Replace("Alpha", String.Empty) : vkey; // for keys: 1, 2, ... 0
        }
    }
}
using System;
using System.Collections.Generic;

using AdvancedGamePad.Core;
using AdvancedGamePad.Wrappers;

namespace AdvancedGamePad.Model
{
    public class SendInput : Win32InputSender
    {
        public override void SendKeyboardInput(ushort[] vKeys, InputType type)
        {
            if (vKeys.Length == 0)
                return;

            List<ushort> mList = new List<ushort>();
            List<ushort> kList = new List<ushort>();

            for (int i = 0; i < vKeys.Length; i++)
            {
                if (vKeys[i] == 1 || vKeys[i] == 2 || vKeys[i] == 4)
                    mList.Add(vKeys[i]);
                else
                    kList.Add(vKeys[i]);
            }

            INPUT[] keyboard = new INPUT[1];
            keyboard[0].type = User32Wrapper.INPUT_KEYBOARD;
            keyboard[0].u.hi = new HARDWAREINPUT();
            keyboard[0].u.mi = new MOUSEINPUT();
            keyboard[0].u.ki = new KEYBDINPUT();
            keyboard[0].u.ki.wScan = 0;
            keyboard[0].u.ki.time = 0;
            keyboard[0].u.ki.dwExtraInfo = IntPtr.Zero;

            INPUT[] mouse = new INPUT[1];
            mouse[0].type = User32Wrapper.INPUT_MOUSE;
            mouse[0].u.hi = new HARDWAREINPUT();
            mouse[0].u.mi = new MOUSEINPUT();
            mouse[0].u.ki = new KEYBDINPUT();
            mouse[0].u.mi.dx = 0;
            mouse[0].u.mi.dy = 0;
            mouse[0].u.mi.mouseData = 0;
            mouse[0].u.mi.time = 0;
            mouse[0].u.mi.dwExtraInfo = IntPtr.Zero;

            switch (type)
            {
                case InputType.Pressed:
                    {
                        if (kList.Count > 0)
                            SetKeyDown(kList.ToArray(), keyboard);
                        if (mList.Count > 0)
                            SetMouseDown(mList.ToArray(), mouse);
                        break;
                    }
                case InputType.Released:
                    {
                        if (kList.Count > 0)
                            SetKeyUp(kList.ToArray(), keyboard);
                        if (mList.Count > 0)
                            SetMouseUp(mList.ToArray(), mouse);
                        break;
                    }
                default:
                    {
                        if (kList.Count > 0)
                            SetKeyDown(kList.ToArray(), keyboard);
                        if (mList.Count > 0)
                            SetMouseDown(mList.ToArray(), mouse);

                        if (kList.Count > 0)
                            SetKeyUp(kList.ToArray(), keyboard);
                        if (mList.Count > 0)
                            SetMouseUp(mList.ToArray(), mouse);
                        break;
                    }
            }
        }

        public override void SendMouseButtonInput(ushort[] vKeys, InputType type)
        {

        }

        public override void SendMouseMoveInput(int x, int y, GamePadStickType type)
        {
            float X = x;
            float Y = y;

            float magnitude = (float)Math.Sqrt(X * X + Y * Y);

            float normalizedX = X / magnitude;
            float normalizedY = Y / magnitude;
            float normalizedMagnitude = 0;

            if (magnitude > (int)XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE)
            {
                if (magnitude > 32767) magnitude = 32767;
                magnitude -= (float)XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE;

                switch (type)
                {
                    case GamePadStickType.Left:
                        if (!Controller.GetProfile().LeftThumbStickAcceleration)
                            normalizedMagnitude = 1 * ((float)Controller.GetProfile().LeftThumbStickSpeed / 10);
                        else
                            normalizedMagnitude = magnitude
                                    / (32767.0f - (float)XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE);
                        break;

                    case GamePadStickType.Right:
                        if (!Controller.GetProfile().RightThumbStickAcceleration)
                            normalizedMagnitude = 1 * ((float)Controller.GetProfile().RightThumbStickSpeed / 10);
                        else
                            normalizedMagnitude = magnitude
                                    / (32767.0f - (float)XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE);
                        break;
                    default:
                        normalizedMagnitude = magnitude
                                / (32767.0f - (float)XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE);
                        break;
                }

                POINT point;
                User32Wrapper.GetCursorPos(out point);

                int refreshRate = 60;
                if (Controller.GetProfile().RepeatMode == RepeatType.HalfDisplayFrequence)
                    refreshRate = 30;

                point.X = (int)(normalizedX * normalizedMagnitude * (refreshRate / 3) + point.X);
                point.Y = (int)(-normalizedY * normalizedMagnitude * (refreshRate / 3) + point.Y);

                User32Wrapper.SetCursorPos(point.X, point.Y);
            }
        }
    }
}

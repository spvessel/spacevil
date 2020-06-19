using System.Runtime.InteropServices;
using AdvancedGamePad.Model;

namespace AdvancedGamePad.Core
{
    public abstract class Win32InputSender : IInputSender
    {
        public Win32InputSender() { }

        public abstract void SendKeyboardInput(ushort[] vKeys, InputType type);

        public abstract void SendMouseButtonInput(ushort[] vKeys, InputType type);

        public abstract void SendMouseMoveInput(int x, int y, GamePadStickType type);

        protected void SetKeyDown(ushort[] keys, INPUT[] ip)
        {
            //press key
            for (int i = 0; i < keys.Length; i++)
            {
                ip[0].u.ki.wVk = keys[i];
                ip[0].u.ki.dwFlags = 0;
                User32Wrapper.SendInput(1, ip, Marshal.SizeOf(typeof(INPUT)));
            }
        }
        protected void SetKeyUp(ushort[] keys, INPUT[] ip)
        {
            //release key
            for (int i = keys.Length - 1; i >= 0; i--)
            {
                ip[0].u.ki.wVk = keys[i];
                ip[0].u.ki.dwFlags = User32Wrapper.KEYEVENTF_KEYUP;
                User32Wrapper.SendInput(1, ip, Marshal.SizeOf(typeof(INPUT)));
            }
        }

        protected void SetMouseDown(ushort[] button, INPUT[] ip)
        {
            //press key
            for (int i = 0; i < button.Length; i++)
            {
                switch (button[i])
                {
                    case 1:
                        {
                            ip[0].u.mi.dwFlags = User32Wrapper.MOUSEEVENTF_LEFTDOWN;
                            break;
                        }
                    case 2:
                        {
                            ip[0].u.mi.dwFlags = User32Wrapper.MOUSEEVENTF_RIGHTDOWN;
                            break;
                        }
                    case 4:
                        {
                            ip[0].u.mi.dwFlags = User32Wrapper.MOUSEEVENTF_MIDDLEDOWN;
                            break;
                        }
                }
                User32Wrapper.SendInput(1, ip, Marshal.SizeOf(typeof(INPUT)));
            }
        }

        protected void SetMouseUp(ushort[] button, INPUT[] ip)
        {
            //release key
            for (int i = button.Length - 1; i >= 0; i--)
            {
                switch (button[i])
                {
                    case 1:
                        {
                            ip[0].u.mi.dwFlags = User32Wrapper.MOUSEEVENTF_LEFTUP;
                            break;
                        }
                    case 2:
                        {
                            ip[0].u.mi.dwFlags = User32Wrapper.MOUSEEVENTF_RIGHTUP;
                            break;
                        }
                    case 4:
                        {
                            ip[0].u.mi.dwFlags = User32Wrapper.MOUSEEVENTF_MIDDLEUP;
                            break;
                        }
                }
                User32Wrapper.SendInput(1, ip, Marshal.SizeOf(typeof(INPUT)));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using AdvancedGamePad.Core;

namespace AdvancedGamePad.Model
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    // [StructLayout(LayoutKind.Explicit)]
    public struct INPUT
    {
        public int type;
        public INPUTUNIOM u;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUTUNIOM
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;
        [FieldOffset(0)]
        public KEYBDINPUT ki;
        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public static class User32Wrapper
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static readonly uint KEYEVENTF_KEYUP = 0x0002;
        public static readonly uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        public static readonly int INPUT_KEYBOARD = 0x01;
        public static readonly int INPUT_MOUSE = 0x00;
        public static readonly int MOUSEEVENTF_MOVE = 0x0001;
        public static readonly uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        public static readonly uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public static readonly uint MOUSEEVENTF_LEFTUP = 0x0004;
        public static readonly uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public static readonly uint MOUSEEVENTF_RIGHTUP = 0x0010;
        public static readonly uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public static readonly uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        public static readonly Dictionary<int, ushort> VKeys = new Dictionary<int, ushort>()
        {
            {(int) HardwareButtons.MouseLeft, 1},
            {(int) HardwareButtons.MouseRight, 2},
            {(int) HardwareButtons.MouseMiddle, 4},
            {(int) HardwareButtons.F1, 112},
            {(int) HardwareButtons.F2, 113},
            {(int) HardwareButtons.F3, 114},
            {(int) HardwareButtons.F4, 115},
            {(int) HardwareButtons.F5, 116},
            {(int) HardwareButtons.F6, 117},
            {(int) HardwareButtons.F7, 118},
            {(int) HardwareButtons.F8, 119},
            {(int) HardwareButtons.F9, 120},
            {(int) HardwareButtons.F10, 121},
            {(int) HardwareButtons.F11, 122},
            {(int) HardwareButtons.F12, 123},
            {(int) HardwareButtons.LeftAlt, 18},
            {(int) HardwareButtons.RightAlt, 18},
            {(int) HardwareButtons.LeftControl, 17},
            {(int) HardwareButtons.RightControl, 17},
            {(int) HardwareButtons.LeftShift, 16},
            {(int) HardwareButtons.RightShift, 16},
            {(int) HardwareButtons.LeftSuper, 91},
            {(int) HardwareButtons.RightSuper, 91},
            {(int) HardwareButtons.Menu, 93},
            {(int) HardwareButtons.Space, 32},
            {(int) HardwareButtons.Escape,27},
            {(int) HardwareButtons.Pause, 19},
            {(int) HardwareButtons.PrintScreen, 154},
            {(int) HardwareButtons.ScrollLock, 145},
            {(int) HardwareButtons.Tab, 9},
            {(int) HardwareButtons.PageUp, 33},
            {(int) HardwareButtons.PageDown, 34},
            {(int) HardwareButtons.End, 35},
            {(int) HardwareButtons.Home, 36},
            {(int) HardwareButtons.Enter, 13},
            {(int) HardwareButtons.Insert, 45},
            {(int) HardwareButtons.Delete, 46},
            {(int) HardwareButtons.CapsLock, 20},
            {(int) HardwareButtons.Left, 37},
            {(int) HardwareButtons.Up, 38},
            {(int) HardwareButtons.Right, 39},
            {(int) HardwareButtons.Down, 40},
            {(int) HardwareButtons.NumLock, 144},
            {(int) HardwareButtons.Numpad0, 96},
            {(int) HardwareButtons.Numpad1, 97},
            {(int) HardwareButtons.Numpad2, 98},
            {(int) HardwareButtons.Numpad3, 99},
            {(int) HardwareButtons.Numpad4, 100},
            {(int) HardwareButtons.Numpad5, 101},
            {(int) HardwareButtons.Numpad6, 102},
            {(int) HardwareButtons.Numpad7, 103},
            {(int) HardwareButtons.Numpad8, 104},
            {(int) HardwareButtons.Numpad9, 105},
            {(int) HardwareButtons.NumpadMultiply, 106},
            {(int) HardwareButtons.NumpadAdd, 107},
            {(int) HardwareButtons.NumpadSubtract, 109},
            {(int) HardwareButtons.NumpadDecimal, 110},
            {(int) HardwareButtons.NumpadDivide, 111},
            {(int) HardwareButtons.NumpadEnter, 13},
            {(int) HardwareButtons.A, 65},
            {(int) HardwareButtons.B, 66},
            {(int) HardwareButtons.C, 67},
            {(int) HardwareButtons.D, 68},
            {(int) HardwareButtons.E, 69},
            {(int) HardwareButtons.F, 70},
            {(int) HardwareButtons.G, 71},
            {(int) HardwareButtons.H, 72},
            {(int) HardwareButtons.I, 73},
            {(int) HardwareButtons.J, 74},
            {(int) HardwareButtons.K, 75},
            {(int) HardwareButtons.L, 76},
            {(int) HardwareButtons.M, 77},
            {(int) HardwareButtons.N, 78},
            {(int) HardwareButtons.O, 79},
            {(int) HardwareButtons.P, 80},
            {(int) HardwareButtons.Q, 81},
            {(int) HardwareButtons.R, 82},
            {(int) HardwareButtons.S, 83},
            {(int) HardwareButtons.T, 84},
            {(int) HardwareButtons.U, 85},
            {(int) HardwareButtons.V, 86},
            {(int) HardwareButtons.W, 87},
            {(int) HardwareButtons.X, 88},
            {(int) HardwareButtons.Y, 89},
            {(int) HardwareButtons.Z, 90},
            {(int) HardwareButtons.Slash, 191},
            {(int) HardwareButtons.Comma, 188},
            {(int) HardwareButtons.Period, 190},
            {(int) HardwareButtons.LeftBracket, 219},
            {(int) HardwareButtons.RightBracket, 221},
            {(int) HardwareButtons.SemiColon, 186},
            {(int) HardwareButtons.Apostrophe, 222},
            {(int) HardwareButtons.Backslash, 220},
            {(int) HardwareButtons.GraveAccent, 192},
            {(int) HardwareButtons.Alpha1, 49},
            {(int) HardwareButtons.Alpha2, 50},
            {(int) HardwareButtons.Alpha3, 51},
            {(int) HardwareButtons.Alpha4, 52},
            {(int) HardwareButtons.Alpha5, 53},
            {(int) HardwareButtons.Alpha6, 54},
            {(int) HardwareButtons.Alpha7, 55},
            {(int) HardwareButtons.Alpha8, 56},
            {(int) HardwareButtons.Alpha9, 57},
            {(int) HardwareButtons.Alpha0, 48},
            {(int) HardwareButtons.Minus, 189},
            {(int) HardwareButtons.Equal, 187},
            {(int) HardwareButtons.Backspace, 8}
        };
    }
}
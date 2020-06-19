using System.Collections.Generic;
using System.Runtime.InteropServices;

using AdvancedGamePad.Core;

namespace AdvancedGamePad.Wrappers
{
    [StructLayout(LayoutKind.Explicit)]
    public struct XInputGamepad
    {
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(0)]
        public ushort WButtons;

        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(2)]
        public byte BLeftTrigger;

        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(3)]
        public byte BRightTrigger;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(4)]
        public short SThumbLX;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(6)]
        public short SThumbLY;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(8)]
        public short SThumbRX;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(10)]
        public short SThumbRY;

        public void ZeroMemory()
        {
            WButtons = 0;
            BLeftTrigger = 0;
            BRightTrigger = 0;
            SThumbLX = 0;
            SThumbLY = 0;
            SThumbRX = 0;
            SThumbRY = 0;
        }
    }

    public struct XInputState
    {
        public uint DwPacketNumber;
        public XInputGamepad Gamepad;

        public void ZeroMemory()
        {
            DwPacketNumber = 0;
            Gamepad = new XInputGamepad();
            Gamepad.ZeroMemory();
        }
    }

    public struct XInputBatteryInformation
    {
        public byte BatteryType;
        public byte BatteryLevel;

        public void ZeroMemory()
        {
            BatteryLevel = 0;
            BatteryType = 0;
        }
    }

    public struct XInputVibration
    {
        public int WLeftMotorSpeed;
        public int WRightMotorSpeed;
    }

    public static class XInputWrapper
    {
        [DllImport("xinput1_4.dll")]
        public static extern int XInputGetState(int dwUserIndex, ref XInputState pState);

        [DllImport("xinput1_4.dll")]
        public static extern uint XInputSetState(int dwUserIndex, ref XInputVibration pVibration);

        [DllImport("xinput1_4.dll")]
        public static extern uint XInputGetBatteryInformation(int dwUserIndex, byte devType, ref XInputBatteryInformation pBatteryInformation);

        public static readonly int XUSER_MAX_COUNT = 4;
        public static readonly int ERROR_SUCCESS = 0x0;

        public enum GP : ushort
        {
            XINPUT_GAMEPAD_A = 0x1000,
            XINPUT_GAMEPAD_B = 0x2000,
            XINPUT_GAMEPAD_X = 0x4000,
            XINPUT_GAMEPAD_Y = 0x8000,
            XINPUT_GAMEPAD_BACK = 0x0020,
            XINPUT_GAMEPAD_START = 0x0010,
            XINPUT_GAMEPAD_DPAD_UP = 0x0001,
            XINPUT_GAMEPAD_DPAD_DOWN = 0x0002,
            XINPUT_GAMEPAD_DPAD_LEFT = 0x0004,
            XINPUT_GAMEPAD_DPAD_RIGHT = 0x0008,
            XINPUT_GAMEPAD_LEFT_SHOULDER = 0x0100,
            XINPUT_GAMEPAD_RIGHT_SHOULDER = 0x0200,
            XINPUT_GAMEPAD_LEFT_THUMB = 0x0040,
            XINPUT_GAMEPAD_RIGHT_THUMB = 0x0080,

            XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE = 8689,
            XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE = 7849,
            XINPUT_GAMEPAD_TRIGGER_THRESHOLD = 30,
            XINPUT_GAMEPAD_RIGHT_TRIGGER = 0x3333,
            XINPUT_GAMEPAD_LEFT_TRIGGER = 0x5555,
        }

        public static readonly Dictionary<GamePadButtonType, GP> GamePadButtons = new Dictionary<GamePadButtonType, GP>()
        {
            {GamePadButtonType.A, GP.XINPUT_GAMEPAD_A},
            {GamePadButtonType.X, GP.XINPUT_GAMEPAD_X},
            {GamePadButtonType.Y, GP.XINPUT_GAMEPAD_Y},
            {GamePadButtonType.B, GP.XINPUT_GAMEPAD_B},
            {GamePadButtonType.Up, GP.XINPUT_GAMEPAD_DPAD_UP},
            {GamePadButtonType.Down, GP.XINPUT_GAMEPAD_DPAD_DOWN},
            {GamePadButtonType.Left, GP.XINPUT_GAMEPAD_DPAD_LEFT},
            {GamePadButtonType.Right, GP.XINPUT_GAMEPAD_DPAD_RIGHT},
            {GamePadButtonType.Start, GP.XINPUT_GAMEPAD_START},
            {GamePadButtonType.Back, GP.XINPUT_GAMEPAD_BACK},
            {GamePadButtonType.LB, GP.XINPUT_GAMEPAD_LEFT_SHOULDER},
            {GamePadButtonType.RB, GP.XINPUT_GAMEPAD_RIGHT_SHOULDER},
            {GamePadButtonType.LT, GP.XINPUT_GAMEPAD_LEFT_TRIGGER},
            {GamePadButtonType.RT, GP.XINPUT_GAMEPAD_RIGHT_TRIGGER},
            {GamePadButtonType.LS, GP.XINPUT_GAMEPAD_LEFT_THUMB},
            {GamePadButtonType.RS, GP.XINPUT_GAMEPAD_RIGHT_THUMB}
        };
    }
}

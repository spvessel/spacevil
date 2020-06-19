using AdvancedGamePad.Core;
using AdvancedGamePad.Wrappers;

namespace AdvancedGamePad.Model
{
    public class XInputGamePad : IGamePad
    {
        private XInputBatteryInformation batteryInfo;
        private XInputState _state;
        public XInputGamePad()
        {
            batteryInfo = new XInputBatteryInformation();
            _state = new XInputState();
        }

        public GamePadBatteryLevel GetBatteryInfo()
        {
            uint result = XInputWrapper.XInputGetBatteryInformation(_gamepadNumber, 0x00, ref batteryInfo);

            switch (batteryInfo.BatteryLevel)
            {
                case 0:
                    return GamePadBatteryLevel.Empty;
                case 1:
                    return GamePadBatteryLevel.Low;
                case 2:
                    return GamePadBatteryLevel.Medium;
                case 3:
                    return GamePadBatteryLevel.Full;
                default:
                    return GamePadBatteryLevel.Empty;
            }
        }

        private int _gamepadNumber = -1;

        public int GetNumber()
        {
            return _gamepadNumber;
        }

        public bool IsConnected()
        {
            int dwResult;
            for (int i = 0; i < XInputWrapper.XUSER_MAX_COUNT; i++)
            {
                dwResult = XInputWrapper.XInputGetState(i, ref _state);

                if (dwResult == XInputWrapper.ERROR_SUCCESS)
                {
                    _gamepadNumber = i;

                    return true;
                }
            }
            _gamepadNumber = -1;

            return false;
        }
    }
}
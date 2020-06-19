using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AdvancedGamePad.Core;
using AdvancedGamePad.Wrappers;

namespace AdvancedGamePad.Model
{
    public delegate void EventBatteryLevel(GamePadBatteryLevel level);
    public class XInputHandler : IGamePadHandler
    {
        public EventBatteryLevel OnBatteryLevelChanged = null;
        private readonly int _delay = 10;
        private bool _isStandby = false;
        private IGamePad _gamepad = null;
        private IInputSender _win32Sender = null;
        private XInputState _state;
        private uint _currentPacketNumber;
        private GamePadBatteryLevel _batteryLevel = GamePadBatteryLevel.Empty;
        private ManualResetEventSlim _permit = new ManualResetEventSlim(false);
        private Task _translator = null;
        private bool _closed = false;

        public XInputHandler(IGamePad gamepad)
        {
            _gamepad = gamepad;

            _win32Sender = new SendInput();

            _translator = new Task(() =>
            {
                Process();
            });
            _translator.Start();
        }

        private bool _isRunning = false;

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void Run()
        {
            if (_closed)
                Restore();

            _isRunning = true;
            _permit.Set();
        }

        public void Stop()
        {
            _isRunning = false;
            _permit.Reset();
        }

        private void Process()
        {
            while (!_closed)
            {
                _permit.Wait();

                if (!_gamepad.IsConnected())
                {
                    Thread.Sleep(500);
                    continue;
                }
                // update battery info
                GamePadBatteryLevel currentBatteryLevel = _gamepad.GetBatteryInfo();
                if (_batteryLevel != currentBatteryLevel)
                {
                    _batteryLevel = currentBatteryLevel;
                    OnBatteryLevelChanged?.Invoke(_batteryLevel);
                }

                int dwResult = XInputWrapper.XInputGetState(_gamepad.GetNumber(), ref _state);
                if (dwResult == XInputWrapper.ERROR_SUCCESS)
                {
                    // send input
                    SetInput(_state, InputType.Clicked, Controller.GetProfile().RepeatMode);
                }
                Thread.Sleep(_delay);
            }
        }

        public void Close()
        {
            _closed = true;
            if (!IsRunning())
                _permit.Set();
        }

        private void Restore()
        {
            _permit.Reset();
            _translator = new Task(() =>
            {
                Process();
            });
            _translator.Start();
        }

        private void SetInput(XInputState st, InputType itype, RepeatType rmode)
        {
            bool isOutDeadzone = false;
            GamePadEventType currentStick = GamePadEventType.LeftThumbStick;

            //Left Thumb Stick
            float LX = st.Gamepad.SThumbLX;
            float LY = st.Gamepad.SThumbLY;

            float magnitudeL = (float)Math.Sqrt(LX * LX + LY * LY);

            float normalizedLX = LX / magnitudeL;
            float normalizedLY = LY / magnitudeL;

            if (magnitudeL > (float)XInputWrapper.GP.XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE)
            {
                isOutDeadzone = true;
                currentStick = GamePadEventType.LeftThumbStick;
            }

            //Right Thumb Stick
            float RX = st.Gamepad.SThumbRX;
            float RY = st.Gamepad.SThumbRY;

            float magnitudeR = (float)Math.Sqrt(RX * RX + RY * RY);

            float normalizedRX = RX / magnitudeR;
            float normalizedRY = RY / magnitudeR;

            if (magnitudeR > (float)XInputWrapper.GP.XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE)
            {
                isOutDeadzone = true;
                currentStick = GamePadEventType.RightThumbStick;
            }

            //lock
            if ((st.Gamepad.WButtons == 48) && isOutDeadzone == false)
            {
                _isStandby = !_isStandby;
                Thread.Sleep(500);
                _currentPacketNumber = st.DwPacketNumber;
            }
            if (_isStandby)
            {
                return;
            }

            if (isOutDeadzone)
                ExecuteAction(st, currentStick, itype);

            //EMULATE BUTTONS
            if (rmode == RepeatType.NoRepeats && _currentPacketNumber == st.DwPacketNumber)
            {
                return;
            }

            //Buttons
            if (st.Gamepad.WButtons != 0)
                ExecuteAction(st, GamePadEventType.Button, itype);

            //Left trigger
            if (st.Gamepad.BLeftTrigger > 254)
                ExecuteAction(st, GamePadEventType.LeftTrigger, itype);

            //Right trigger
            if (st.Gamepad.BRightTrigger > 254)
                ExecuteAction(st, GamePadEventType.RightTrigger, itype);

            st.ZeroMemory();
            XInputWrapper.XInputGetState(_gamepad.GetNumber(), ref st);
            _currentPacketNumber = st.DwPacketNumber;
        }

        private ushort[] GetScancodeSequense(XInputState st, GamePadEventType type)
        {
            string[] keystroke = new string[] { };
            switch (type)
            {
                case GamePadEventType.Button:
                    keystroke = Parse.Keystroke(Parse.KeysToString(Controller.GetProfile(), (XInputWrapper.GP)st.Gamepad.WButtons));
                    break;
                case GamePadEventType.RightTrigger:
                    keystroke = Parse.Keystroke(Parse.KeysToString(Controller.GetProfile(), (XInputWrapper.GP)0x3333));
                    break;
                case GamePadEventType.LeftTrigger:
                    keystroke = Parse.Keystroke(Parse.KeysToString(Controller.GetProfile(), (XInputWrapper.GP)0x5555));
                    break;
            }
            List<ushort> result = new List<ushort>();
            foreach (var item in keystroke)
            {
                HardwareButtons code = (HardwareButtons)Enum.Parse(typeof(HardwareButtons), item);
                if (User32Wrapper.VKeys.TryGetValue((int)code, out ushort value))
                    result.Add(value);
            }

            return result.ToArray();
        }

        private void ExecuteAction(XInputState st, GamePadEventType etype, InputType itype)
        {
            if (etype == GamePadEventType.RightThumbStick)
            {
                _win32Sender.SendMouseMoveInput(st.Gamepad.SThumbRX, st.Gamepad.SThumbRY, GamePadStickType.Right);
            }
            else if (etype == GamePadEventType.LeftThumbStick)
            {
                _win32Sender.SendMouseMoveInput(st.Gamepad.SThumbLX, st.Gamepad.SThumbLY, GamePadStickType.Left);
            }
            else
            {
                ushort[] sequense = GetScancodeSequense(st, etype);
                if (sequense.Length == 0)
                    return;

                _win32Sender.SendKeyboardInput(sequense, itype);
            }
        }
    }
}
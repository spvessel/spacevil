namespace AdvancedGamePad.Core
{
    public interface IInputSender
    {
        void SendKeyboardInput(ushort[] vKeys, InputType type);

        void SendMouseButtonInput(ushort[] vKeys, InputType type);

        void SendMouseMoveInput(int x, int y, GamePadStickType type);
    }
}
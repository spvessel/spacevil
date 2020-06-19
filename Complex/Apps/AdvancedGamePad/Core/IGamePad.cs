namespace AdvancedGamePad.Core
{
    public interface IGamePad
    {
        int GetNumber();
        bool IsConnected();
        GamePadBatteryLevel GetBatteryInfo();
    }
}
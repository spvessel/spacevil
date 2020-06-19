namespace AdvancedGamePad.Core
{
    public interface IGamePadHandler
    {
        bool IsRunning();
        void Run();
        void Stop();
        void Close();
    }
}
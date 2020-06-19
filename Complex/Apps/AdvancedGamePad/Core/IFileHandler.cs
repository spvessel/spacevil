namespace AdvancedGamePad.Core
{
    public delegate void EventIOFails(string message, string title);
    
    public interface IFileHandler<T>
    {
        T Load(string filePath);

        bool Save(T item, string filePath);

        T GetDefault();
    }
}
namespace Sysmeta.Xbmc.Remote.Services
{
    public interface ICache
    {
        void Add(string key, object value);
        T Get<T>(string key);
        bool HasValue(string key);
        void Clear();
    }
}
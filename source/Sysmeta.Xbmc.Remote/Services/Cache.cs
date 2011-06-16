namespace Sysmeta.Xbmc.Remote.Services
{
    using System.Collections.Generic;

    public class Cache : ICache
    {
        private Dictionary<string, object> cache = new Dictionary<string, object>(); 

        public void Add(string key, object value)
        {
            cache[key] = value;
        }

        public T Get<T>(string key)
        {
            return (T)this.cache[key];
        }

        public bool HasValue(string key)
        {
            return this.cache.ContainsKey(key);
        }

        public void Clear()
        {
            this.cache.Clear();
        }
    }
}
using Microsoft.Extensions.Caching.Memory;

namespace TimesheetManagement.Helpers
{
    public class CacheManager : ICacheManager
    {
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        void ICacheManager.Add(string key, object value)
        {
            _cache.Set(key, value);
        }

        T ICacheManager.Get<T>(string key)
        {
            return _cache.TryGetValue(key, out var value) ? (T)value : null;
        }

        void ICacheManager.Clear(string key)
        {
            _cache.Remove(key);
        }
    }
}
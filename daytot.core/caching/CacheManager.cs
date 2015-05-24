using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using daytot.core.helpers;

namespace daytot.core.caching
{
    public class CacheManager
    {
        private ICache _cache = null;
        public ICache Cache { get { return _cache; } }

        public CacheManager()
            : this("memcached", string.Empty, string.Empty)
        {
        }

        public CacheManager(string provider, string sectionName, string prefixKey)
        {
            if (string.IsNullOrEmpty(provider))
            {
                provider = "memcached";
            }
            provider = provider.ToLower();
            if (provider == "memcached")
            {
                _cache = new MemcachedProvider();
                _cache.Initialize(sectionName, prefixKey);
                
            }
        }

        private static CacheManager _defaultInstanse = null;
        public static ICache CacheClient
        {
            get
            {
                if (_defaultInstanse == null)
                {
                    CreateDefault();
                }
                return _defaultInstanse._cache;
            }
        }

        /// <summary>
        /// Sử dụng Cach được cài đặt appSettings với các key: CacheManager_Provider, CacheManager_Session, CacheManager_PrefixKey
        /// </summary>
        /// <returns></returns>
        public static CacheManager CreateDefault()
        {
            if (_defaultInstanse == null)
            {
                string provider = Configuration.AppSettings("CacheManager_Provider", string.Empty);
                string sectionName = Configuration.AppSettings("CacheManager_Session", string.Empty);
                string prefixKey = Configuration.AppSettings("CacheManager_PrefixKey", string.Empty);

                _defaultInstanse = new CacheManager(provider, sectionName, prefixKey);
            }

            return _defaultInstanse;
        }

        public static bool RemoveCache(string key)
        {
            if (_defaultInstanse == null) CreateDefault();

            return _defaultInstanse.Cache.Remove(key);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace daytot.core.caching
{
    /// <summary>
    /// Cache Base
    /// </summary>
    public abstract class CacheBase : ICache
    {
        protected string PrefixKey = string.Empty;
        public abstract void Initialize(string sectionName, string prefixKey);

        protected string GetFullKey(string key)
        {
            if (string.IsNullOrEmpty(this.PrefixKey) == true)
                return key;

            return this.PrefixKey + "_" + key;
        }

        #region Get Value
        public abstract object this[string key]{get;}
        public abstract object this[string key, bool usedHttpRuntimeCache] { get; }

        /// <summary>
        /// Trả về dữ liệu từ Cache (object)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetValue(string key)
        {
            return this[key];
        }

        public object GetValue(string key, bool usedHttpRuntimeCache)
        {
            return this[key, usedHttpRuntimeCache];
        }
        
        /// <summary>
        /// Trả về dữ liệu từ Cache (string)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public string GetValue(string key, string def)
        {
            object v = this[key];
            return (v ?? def).ToString();
        }

        public string GetValue(string key, string def, bool usedHttpRuntimeCache)
        {
            object v = this[key, usedHttpRuntimeCache];
            return (v ?? def).ToString();
        }
        
        /// <summary>
        /// Trả về dữ liệu từ Cache(int)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public int GetValue(string key, int def)
        {
            object v = this[key];
            return Convert.ToInt32(v ?? def);
        }

        public int GetValue(string key, int def, bool usedHttpRuntimeCache)
        {
            object v = this[key, usedHttpRuntimeCache];
            return Convert.ToInt32(v ?? def);
        }
        
        /// <summary>
        /// Trả về dữ liệu từ Cache(long)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public long GetValue(string key, long def)
        {
            object v = this[key];
            return Convert.ToInt64(v ?? def);
        }

        public long GetValue(string key, long def, bool usedHttpRuntimeCache)
        {
            object v = this[key, usedHttpRuntimeCache];
            return Convert.ToInt64(v ?? def);
        }
        
        /// <summary>
        /// Trả về dữ liệu từ Cache(DateTime)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public DateTime GetValue(string key, DateTime def)
        {
            object v = this[key];
            return Convert.ToDateTime(v ?? def);
        }

        public DateTime GetValue(string key, DateTime def, bool usedHttpRuntimeCache)
        {
            object v = this[key, usedHttpRuntimeCache];
            return Convert.ToDateTime(v ?? def);
        }
        #endregion

        #region Add Value
        protected abstract bool AddCache(string key, object v, bool usedHttpRuntimeCache);
        protected abstract bool AddCache(string key, object v, DateTime absoluteExpiration, bool usedHttpRuntimeCache);

        public void Add(string key, object v)
        {
            this.AddCache(key, v, false);
        }
        public void Add(string key, object v, bool usedHttpRuntimeCache)
        {
            this.AddCache(key, v, usedHttpRuntimeCache);
        }
        public void Add(string key, object v, int minutes)
        {
            this.AddCache(key, v, DateTime.UtcNow.AddMinutes(minutes), false);
        }
        public void Add(string key, object v, int minutes, bool usedHttpRuntimeCache)
        {
            this.AddCache(key, v, DateTime.UtcNow.AddMinutes(minutes), usedHttpRuntimeCache);
        }
        public void Add(string key, object v, DateTime absoluteExpiration)
        {
            this.AddCache(key, v, absoluteExpiration.ToUniversalTime(), false);
        }
        public void Add(string key, object v, DateTime absoluteExpiration, bool usedHttpRuntimeCache)
        {
            this.AddCache(key, v, absoluteExpiration.ToUniversalTime(), usedHttpRuntimeCache);
        }
        #endregion

        #region Add HttpRuntime Cache
        protected void AddHttpRuntimeCache(string key, object v)
        {
            HttpRuntime.Cache.Insert(key,v);
        }
        protected void AddHttpRuntimeCache(string key, object v, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Add(key,
                    v, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Normal, null);
        }
        #endregion


        public abstract bool Remove(string key);
        public abstract void FlushAll();
    }

    [Serializable]
    public class CacheHashtable
    {
        public Hashtable CacheClient = null;
        private string _key;
        public string Key { get { return _key; } }
        public string KeyVersion { get { return _key + "_VERSION"; } }

        private long _cacheVersion = 0;
        /// <summary>
        /// Phiên bản Cache
        /// </summary>
        public long CacheVersion
        {
            get { return _cacheVersion; }
            set { _cacheVersion = value; }
        }

        public CacheHashtable(string key)
        {
            this.CacheClient = Hashtable.Synchronized(new Hashtable());
            this._key = "HASHTABLE_" + key.ToUpper();
        }

        public void Add(object key, object value)
        {
            lock (CacheClient.SyncRoot)
            {
                if (CacheClient.ContainsKey(key) == false)
                {
                    CacheClient.Add(key, value);
                }
                else
                {
                    CacheClient[key] = value;
                }
            }
        }

        public object Get(object key)
        {
            return CacheClient[key];
        }

        public void Remove(object key)
        {
            lock (CacheClient.SyncRoot)
            {
                if (CacheClient.ContainsKey(key))
                {
                    CacheClient.Remove(key);
                }
            }
        }

    }
    public enum CacheExpiration : int
    {
        FiveMinutes = 5,
        TenMinutes = 10,
        Default = 30,
        OneHour = 60,
        OneDay = 1440,
        OneWeek = 10080,
        OneMonth = 43200,
        OneYear = 525600
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using Enyim.Reflection;

namespace daytot.core.caching
{
    public class MemcachedProvider : CacheBase, ICache, IDisposable
    {
        private IMemcachedClient client;

        public MemcachedProvider()
        {

        }

        public override void Initialize(string sectionName, string prefixKey)
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                sectionName = "enyim.com/memcached";
            }
            client = new MemcachedClient(sectionName);
            
            this.PrefixKey = prefixKey;
        }

        public override object this[string key]
        {
            get
            {
                return client.Get(this.GetFullKey(key));
            }
        }

        public override object this[string key, bool usedHttpRuntimeCache]
        {
            get
            {
                object v = null;
                if (usedHttpRuntimeCache == true)
                {
                    v = HttpRuntime.Cache[key];
                }

                if (v != null)
                {
                    return v;
                }

                return this[key];
            }
        }

        protected override bool AddCache(string key, object v, bool usedHttpRuntimeCache)
        {
            if (usedHttpRuntimeCache == true)
            {
                this.AddHttpRuntimeCache(key, v);
            }

            return client.Store(StoreMode.Set, this.GetFullKey(key), v);
        }

        protected override bool AddCache(string key, object v, DateTime absoluteExpiration, bool usedHttpRuntimeCache)
        {
            if (usedHttpRuntimeCache == true)
            {
                this.AddHttpRuntimeCache(key, v, absoluteExpiration);
            }

            return client.Store(StoreMode.Set, this.GetFullKey(key), v, absoluteExpiration);
        }

        public override bool Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);

            return client.Remove(this.GetFullKey(key));
        }

        public override void FlushAll()
        {
            //throw new NotImplementedException();
            client.FlushAll();
        }


        #region Dispose
        ~MemcachedProvider()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
		protected virtual void Dispose(bool disposing)
        {
            //if(disposing)
            //{
            //}
        }
        #endregion 
    }
}

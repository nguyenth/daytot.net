using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace daytot.core.caching
{
    public interface ICache
    {
        void Initialize(string sectionName, string prefixKey);

        void Add(string key, object v);
        void Add(string key, object v, bool usedHttpRuntimeCache);
        void Add(string key, object v, int minutes);
        void Add(string key, object v, int minutes, bool usedHttpRuntimeCache);
        void Add(string key, object v, DateTime absoluteExpiration);
        void Add(string key, object v, DateTime absoluteExpiration, bool usedHttpRuntimeCache);

        bool Remove(string key);
        void FlushAll();

        object GetValue(string key);
        object GetValue(string key, bool usedHttpRuntimeCache);
        string GetValue(string key, string def);
        string GetValue(string key, string def, bool usedHttpRuntimeCache);
        int GetValue(string key, int def);
        int GetValue(string key, int def, bool usedHttpRuntimeCache);
        long GetValue(string key, long def);
        long GetValue(string key, long def, bool usedHttpRuntimeCache);
        DateTime GetValue(string key, DateTime def);
        DateTime GetValue(string key, DateTime def, bool usedHttpRuntimeCache);

    }
}

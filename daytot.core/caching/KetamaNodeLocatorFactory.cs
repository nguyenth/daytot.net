using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace daytot.core.caching
{
    /// <summary>
    /// Implements Ketama cosistent hashing, compatible with the "spymemcached" Java client
    /// </summary>
    public class KetamaNodeLocatorFactory : IProviderFactory<IMemcachedNodeLocator>
    {
        private string hashName;

        void IProvider.Initialize(Dictionary<string, string> parameters)
        {
            //ConfigurationHelper.TryGetAndRemove(parameters, "hashName", out this.hashName, false);
            TryGetAndRemove(parameters, "hashName", out this.hashName, false);
        }

        IMemcachedNodeLocator IProviderFactory<IMemcachedNodeLocator>.Create()
        {
            return new KetamaNodeLocator(this.hashName);
        }

        internal static bool TryGetAndRemove(Dictionary<string, string> dict, string name, out string value, bool required)
        {
            if (dict.TryGetValue(name, out value))
            {
                dict.Remove(name);

                if (!String.IsNullOrEmpty(value))
                    return true;
            }

            if (required)
                throw new System.Configuration.ConfigurationErrorsException("Missing parameter: " + (String.IsNullOrEmpty(name) ? "element content" : name));

            return false;
        }
    }
}

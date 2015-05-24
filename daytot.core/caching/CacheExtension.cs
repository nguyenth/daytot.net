using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using daytot.core.helpers;

namespace daytot.core.caching
{
    public static class CacheExtension
    {

        private static CacheManager _cacheClient = null;
        private static CacheManager CacheClient
        {
            get
            {
                if (_cacheClient == null)
                {
                    _cacheClient = CacheManager.CreateDefault();
                }
                return _cacheClient;
            }
        }
        

        private static bool? _UseMemoryCache = null;

        /// <summary>
        /// AppSettings.UseMemoryCache
        /// </summary>
        public static bool UseMemoryCache
        {
            get
            {

                if (_UseMemoryCache.HasValue == false)
                {
                    _UseMemoryCache = Configuration.AppSettings("UseMemoryCache", false);
                }
                return _UseMemoryCache.Value;
            }
        }

        #region Cache: IQueryable, IEnumerable, List
        public static List<T> MemoryCache<T>(this System.Linq.IQueryable<T> q, string cacheId)
        {
            try
            {
                List<T> objCache = (List<T>)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);

                if (objCache == null)
                {
                    objCache = q.ToList();

                    CacheClient.Cache.Add(cacheId, objCache, UseMemoryCache);
                }

                return objCache;
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-IQueryable: " + cacheId, ex);
            }

            return null;
        }
        public static List<T> MemoryCache<T>(this System.Linq.IQueryable<T> q, string cacheId, int minutes)
        {
            try
            {
                List<T> objCache = (List<T>)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);

                if (objCache == null)
                {
                    objCache = q.ToList();

                    CacheClient.Cache.Add(cacheId, objCache, minutes, UseMemoryCache);
                }

                return objCache;
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-IQueryable-Minutes: " + cacheId, ex);
            }

            return null;
        }
        public static List<T> MemoryCache<T>(this System.Linq.IQueryable<T> q, string cacheId, CacheExpiration expiration)
        {
            return MemoryCache(q, cacheId, (int)expiration);
        }

        public static List<T> MemoryCache<T>(this IEnumerable<T> q, string cacheId)
        {
            try
            {
                List<T> objCache = (List<T>)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);

                if (objCache == null)
                {
                    objCache = q.ToList();

                    CacheClient.Cache.Add(cacheId, objCache, UseMemoryCache);
                }

                return objCache;
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-IEnumerable: " + cacheId, ex);
            }

            return null;
        }
        public static List<T> MemoryCache<T>(this IEnumerable<T> q, string cacheId, int minutes)
        {
            try
            {
                List<T> objCache = (List<T>)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);

                if (objCache == null)
                {
                    objCache = q.ToList();

                    CacheClient.Cache.Add(cacheId, objCache, minutes, UseMemoryCache);
                }

                return objCache;
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-IEnumerable-Minutes: " + cacheId, ex);
            }

            return null;
        }
        public static List<T> MemoryCache<T>(this IEnumerable<T> q, string cacheId, CacheExpiration expiration)
        {
            return MemoryCache(q, cacheId, (int)expiration);
        }

        public static List<T> MemoryCache<T>(this List<T> q, string cacheId)
        {
            try
            {
                List<T> objCache = (List<T>)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);

                if (objCache == null)
                {
                    objCache = q;

                    CacheClient.Cache.Add(cacheId, objCache, UseMemoryCache);
                }

                return objCache;
            }
            catch (Exception ex)
            {
               Logger.Error("CacheExtension.MemoryCache-List: " + cacheId, ex);
            }

            return null;
        }
        public static List<T> MemoryCache<T>(this List<T> q, string cacheId, int minutes)
        {
            try
            {
                List<T> objCache = (List<T>)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);

                if (objCache == null)
                {
                    objCache = q;
                    
                    CacheClient.Cache.Add(cacheId, objCache, minutes, UseMemoryCache);
                }

                return objCache;
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-List-Minutes: " + cacheId, ex);
            }

            return null;
        }
        public static List<T> MemoryCache<T>(this List<T> q, string cacheId, CacheExpiration expiration)
        {
            return MemoryCache(q, cacheId, (int)expiration);
        }

        public static void MemoryCache<T>(this T obj, string cacheId)
        {
            try
            {
                if (obj != null)
                {
                    CacheClient.Cache.Add(cacheId, obj, UseMemoryCache);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-T: " + cacheId, ex);
            }
        }

        public static void MemoryCache<T>(this T obj, string cacheId, CacheHashtable objHashtable, int hashtableId)
        {
            try
            {
                if (obj != null)
                {
                    // Hashtable Cache
                    if (objHashtable != null)
                    {
                        objHashtable.Add(hashtableId, obj);
                    }

                    // Cache Provider
                    CacheClient.Cache.Add(cacheId, obj, UseMemoryCache);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-T: " + cacheId, ex);
            }
        }

        public static void MemoryCache<T>(this T obj, string cacheId, CacheHashtable objHashtable, string hashtableId)
        {
            try
            {
                if (obj != null)
                {
                    // Hashtable Cache
                    if (objHashtable != null)
                    {
                        objHashtable.Add(hashtableId, obj);
                    }

                    // Cache Provider
                    CacheClient.Cache.Add(cacheId, obj, UseMemoryCache);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-T: " + cacheId, ex);
            }
        }

        public static void MemoryCache<T>(this T obj, string cacheId, int minutes)
        {
            try
            {
                if (obj != null)
                {
                    CacheClient.Cache.Add(cacheId, obj, minutes, UseMemoryCache);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("CacheExtension.MemoryCache-T-Minutes: " + cacheId, ex);
            }
        }
        public static void MemoryCache<T>(this T obj, string cacheId, CacheExpiration expiration)
        {
            MemoryCache(obj, cacheId, (int)expiration);
        }

        #endregion

        public static List<T> GetCache<T>(this System.Linq.IQueryable<T> q, string cacheId)
        {
            return (List<T>)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);
        }
        public static T GetCache<T>(this T obj, string cacheId)
        {
            return (T)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);
        }

        public static T GetCache<T>(this T obj, string cacheId, CacheHashtable objHashtable, int hashtableId)
        {
            if (objHashtable != null && objHashtable.CacheClient.ContainsKey(hashtableId))
            {
                return (T)objHashtable.CacheClient[hashtableId];
            }
            return (T)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);
        }

        public static T GetCache<T>(this T obj, string cacheId, CacheHashtable objHashtable, string hashtableId)
        {
            if (objHashtable != null && objHashtable.CacheClient.ContainsKey(hashtableId))
            {
                return (T)objHashtable.CacheClient[hashtableId];
            }
            return (T)CacheClient.Cache.GetValue(cacheId, UseMemoryCache);
        }

    }
}

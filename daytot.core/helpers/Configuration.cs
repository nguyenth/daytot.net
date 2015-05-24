using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace daytot.core.helpers
{
    public class Configuration
    {
        /// <summary>
        /// Lấy giá trị AppSetting trên web.conf
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string AppSettings(string key, string def = "")
        {
            string v = ConfigurationManager.AppSettings[key];
            if (v == null) v = def;
            return v;
        }

        /// <summary>
        /// Lấy giá trị kiểu số nguyên AppSetting trên web.conf
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int AppSettings(string key, int def)
        {
            int result = 0;
            string v = AppSettings(key, def.ToString());
            if (int.TryParse(v, out result)) return result;
            return def;
        }

        // <summary>
        /// Lấy giá trị kiểu boolean AppSetting trên web.conf
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static bool AppSettings(string key, bool def)
        {
            bool result = false;
            string v = AppSettings(key, string.Empty);
            if (bool.TryParse(v, out result)) return result;
            return def;
        }

    }
}

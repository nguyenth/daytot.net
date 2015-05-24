using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Newtonsoft.Json;

namespace daytot.core.utils
{
    public static class SerializeHelper
    {

        /// <summary>
        /// Serialize một object không định kiểu thành string formmat dạng json
        /// </summary>
        /// <param name="obj">Object cần Serialize</param>
        /// <returns>String với format json</returns>
        public static string ToJson(this object obj)
        {
            if (obj == null)
                return null;
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserialize một string thành một object kiểu T
        /// </summary>
        /// <typeparam name="T">Loại đối tượng</typeparam>
        /// <param name="json">String dạng json</param>
        /// <returns>Object kiểu T</returns>
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json == null ? string.Empty : json);
        }

        /// <summary>
        /// Serialize một đối tượng danh sách thành string dạng XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToXml<T>(this List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<root>");
            foreach (T item in list)
            {
                Type type = typeof(T);
                PropertyInfo[] pros = type.GetProperties();
                string row = "<row ";
                foreach (var pro in pros)
                {
                    object val = pro.GetValue(item);
                    if (val != null)
                    {
                        row += pro.Name + "=\"" + val + "\" ";
                    }
                }
                row += " />";
                sb.Append(row);
            }
            sb.Append("</root>");

            return sb.ToString();
        }

    }
}

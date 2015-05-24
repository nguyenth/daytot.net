using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace daytot.core.utils
{
    public class Common
    {
        /// <summary>
        /// Tạo mã một chuổi số ngẫu nhiên.
        /// </summary>
        /// <param name="length">Chiều dài chuổi ngẩu nhiên</param>
        /// <returns></returns>
        public static string RandomCode(int length)
        {
            var sb = new StringBuilder();
            Random rnd = new Random();
            const string strDefault = "0123456789QWERTYUIOPASDFGHJKLZXCVBNM";
            const string strFirstChar = "0123456789QWERTYUIOPASDFGHJKLZXCVBNM";
            int index_Firstchar = rnd.Next(0, strFirstChar.Length);
            var firstchar = strFirstChar.Substring(index_Firstchar, 1);
            sb.Append(firstchar);

            for (var i = 0; i < length - 1; i++)
            {
                int index_nextchar = rnd.Next(0, strDefault.Length);
                sb.Append(strDefault.Substring(index_nextchar, 1));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Lấy hình mặc định cho một cách sử dụng
        /// </summary>
        /// <param name="usage"></param>
        /// <returns></returns>
        public static string DefaultImage(int usage) {
            string url = "";
            switch(usage){
                case core.Consts.MEDIA_USAGE_AVATAR:
                    url = core.Consts.DEFAULT_AVATAR_IMAGE;
                    break;
                case core.Consts.MEDIA_USAGE_COURSEINTRO:
                    url = core.Consts.DEFAULT_INTRO_IMAGE;
                    break;
            }
            return System.IO.Path.Combine(@"/uploads", url);
        }

        /// <summary>
        /// Lấy ràng buột hình ảnh theo cách sử dụng
        /// </summary>
        /// <param name="usage"></param>
        /// <returns></returns>
        public static SizeRule GetSizeRule(int usage) {
            /*Theo tỉ lệ 16:9*/
            switch (usage)
            {
                case Consts.MEDIA_USAGE_AVATAR:
                    return new SizeRule { 
                        MaxW = 200,
                        MinW = 100,
                        MaxH = 200,
                        MinH = 100
                    };
                case Consts.MEDIA_USAGE_COURSEINTRO:
                    return new SizeRule
                    {
                        MaxW = 1280,
                        MinW = 480,
                        MaxH = 720,
                        MinH = 272
                    };
                default:
                    return new SizeRule
                    {
                        MaxW = 1280,
                        MinW = 480,
                        MaxH = 720,
                        MinH = 272
                    };
            }
        }
    }

    public class SizeRule {

        /// <summary>
        /// Chiều rộng tối đa
        /// </summary>
        public int MaxW { get; set; }

        /// <summary>
        /// Chiều rộng tối thiểu
        /// </summary>
        public int MinW { get; set; }

        /// <summary>
        /// Chiều cao tối đa
        /// </summary>
        public int MaxH { get; set; }

        /// <summary>
        /// Chiều cao tối thiểu
        /// </summary>
        public int MinH { get; set; }
    }
}

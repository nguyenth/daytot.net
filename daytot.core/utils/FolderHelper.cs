using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace daytot.core.utils
{
    public class FolderHelper
    {
        /// <summary>
        /// Tạo link đến file media
        /// VD: referId=123456, created=23/07/2013, fileExt=.jpg
        /// return 201307/23/123/123456_{random}.jpg
        /// </summary>
        /// <param name="referId"></param>
        /// <param name="created"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public static string GetMediaVirtualPath(int referId, DateTime created, string fileExt)
        {

            string ymd = created.ToString("yyyyMMdd").Insert(6, "/");
            string sub = GetMediaPath(referId, "/");
            if (fileExt.StartsWith(".") == false) fileExt = Path.GetExtension(fileExt);

            return string.Format("{0}/{1}{2}_{3}{4}", ymd, sub, referId.ToString(), DateTime.Now.Ticks.ToString(), fileExt.ToLower());
        }

        /// <summary>
        /// GID=123456 => return: 123/
        /// </summary>
        /// <param name="GID"></param>
        /// <param name="sepa"></param>
        /// <returns></returns>
        public static string GetMediaPath(long GID, string sepa)
        {
            string result = GID.ToString("D12").Substring(6, 3);
            return (result == "000" ? string.Empty : result + sepa);
        }
    }
}

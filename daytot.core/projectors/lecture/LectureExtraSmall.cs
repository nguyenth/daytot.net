using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.projectors
{
    public class LectureExtraSmall
    {
        /// <summary>
        /// Mã bài giảng
        /// </summary>
        public int LectureId { get; set; }

        /// <summary>
        /// Mã chương
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Tên bài giảng
        /// </summary>
        public string LectureTitle { get; set; }

        /// <summary>
        /// % hoàn thành
        /// </summary>
        public int Completed { get; set; }
    }
}

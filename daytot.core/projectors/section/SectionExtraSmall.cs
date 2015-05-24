using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.projectors
{
    public class SectionExtraSmall
    {
        /// <summary>
        /// Mã phần
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Mã khóa học
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Tên chương
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Danh sách bài giảng
        /// </summary>
        public ICollection<LectureExtraSmall> Lectures { get; set; }

    }
}

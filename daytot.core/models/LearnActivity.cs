using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    public class LearnActivity
    {
        /// <summary>
        /// Mã đăng ký
        /// </summary>
        [Column(Order=1), Key]
        public int EnrollId { get; set; }

        /// <summary>
        /// Mã bài giảng
        /// </summary>
        [Column(Order = 2), Key]
        public int LectureId { get; set; }

        /// <summary>
        /// Thời gian làm bài tình theo giây
        /// </summary>
        public int SpentTime { get; set; }

        /// <summary>
        /// Hoàn thành khóa học.
        /// </summary>
        public int Completed { get; set; }

        /// <summary>
        /// Thời gian học gần nhất
        /// </summary>
        public DateTime? LastView { get; set; }
        
        /// <summary>
        /// Số lần học bài
        /// </summary>
        public int Attemps { get; set; }

    }
}

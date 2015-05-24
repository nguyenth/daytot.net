using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Newtonsoft.Json;

namespace daytot.core.models
{
    public class Assignment
    {
        /// <summary>
        /// Mã giáo viên
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Mã khóa học
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Vai trò
        /// Bit 1: Giáo viên chính
        /// Bit 2: Giáo viên hổ trợ bài tập
        /// </summary>
        public int Role { get; set; }

        [ForeignKey("UserId")]
        public virtual User Teacher { get; set; }
        
        [JsonIgnore]
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}

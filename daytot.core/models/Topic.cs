using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace daytot.core.models
{
    public class Topic
    {
        /// <summary>
        /// Mã chủ đề
        /// </summary>
        [Key]
        public int TopicId { get; set; }
        /// <summary>
        /// Tên chủ đề
        /// </summary>
        public string TopicTitle { get; set; }
        /// <summary>
        /// Môn học
        /// </summary>
        public int SubjectId { get; set; }
        /// <summary>
        /// Phân cấp trình độ
        /// </summary>
        public int ClassLevelId { get; set; }

        /// <summary>
        /// Thông tin môn học
        /// </summary>
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        /// <summary>
        /// Lớp học
        /// </summary>
        [ForeignKey("ClassLevelId")]
        public ClassLevel Class { get; set; }
        
    }
}

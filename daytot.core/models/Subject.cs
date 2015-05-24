using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    public class Subject
    {
        /// <summary>
        /// Mã môn học do hệ thống sinh ra
        /// </summary>
        [Key]
        public int SubjectId { get; set; }

        /// <summary>
        /// Tên môn học
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Url thân thiện
        /// </summary>
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        [Column(TypeName = "varchar")]
        public string SEOUrl { get; set; }

        /// <summary>
        /// Tiêu đề dùng cho SEO
        /// </summary>
        [StringLength(150, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string SEOTitle { get; set; }

        /// <summary>
        /// Mô tả dùng cho SEO
        /// </summary>
        [StringLength(250, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string SEODescription { get; set; }
    }
}

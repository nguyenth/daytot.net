using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace daytot.core.models
{
    public class ClassLevel
    {
        /// <summary>
        /// Mã lớp
        /// </summary>
        [Key]
        public int ClassLevelId { get; set; }

        /// <summary>
        /// Tên lớp
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(resources.Validations))]
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string ClassName { get; set; }

        /// <summary>
        /// Cấp độ. cấp độ lớp sẽ bao gồm cấp độ nhỏ
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Url thân thiện
        /// </summary>
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        [Column(TypeName="varchar")]
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

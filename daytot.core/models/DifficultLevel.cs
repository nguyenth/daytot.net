using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace daytot.core.models
{
    public class DifficultLevel
    {
        /// <summary>
        /// Mã mức độ khó
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// tên mức độ khóa
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Mức độ khó,Giá trị này càng lớn càng khó
        /// </summary>
        public int Level { get; set; }
    }
}

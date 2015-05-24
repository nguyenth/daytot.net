using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace daytot.core.models
{
    public class MediaServer
    {
        /// <summary>
        /// Mã server lưu hình.
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int ServerId { get; set; }

        /// <summary>
        /// Đường dẫn lưu trữ hình.
        /// </summary>
        [Column(TypeName = "varchar")]
        public string PhysicalPath { get; set; }

        /// <summary>
        /// Đường dẫn chỉ tới application hiển thị hình ảnh
        /// </summary>
        [Column(TypeName = "varchar")]
        public string ApplicationPath { get; set; }
    }
}

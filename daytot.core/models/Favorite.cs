using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    public class Favorite
    {
        /// <summary>
        /// Mã tàu khoản
        /// </summary>
        [Column(Order = 0), Key]
        public int UserId { get; set; }

        /// <summary>
        /// Mã đối tượng lưu
        /// </summary>
        [Column(Order = 1), Key]
        public int ReferId { get; set; }

        /// <summary>
        /// Loại đối tượng lưu
        /// </summary>
        [Column(Order = 2), Key]
        public int ReferTypeId { get; set; }

        /// <summary>
        /// Ngày lưu
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Địa chỉ IP
        /// </summary>
        public string Ip { get; set; }
    }
}

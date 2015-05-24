using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    /// <summary>
    /// Thông tin 1 connection trong Room.
    /// </summary>
    public class OnlineConnection
    {

        /// <summary>
        /// Connection's id
        /// </summary>
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public string ConnectionID { get; set; }

        /// <summary>
        /// Khóa ngoại từ bảng user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User agent
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// Trạng thái online
        /// </summary>
        public bool Connected { get; set; }
    }
}

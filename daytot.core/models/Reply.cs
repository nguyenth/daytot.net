using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{

    public class Reply
    {
        /// <summary>
        /// Mã trả lời
        /// </summary>
        [Key]
        public int ReplyId { get; set; }

        /// <summary>
        /// Comment được trả lời
        /// </summary>
        public int AskId { get; set; }

        /// <summary>
        /// Mã người trả lời
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Thông tin người trả lời
        /// </summary>
        [ForeignKey("UserId")]
        public User Answer { get; set; }
        
        /// <summary>
        /// Nội dung trả lời
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Description { get; set; }

        /// <summary>
        /// Đồng ý với câu trả lời này
        /// </summary>
        public int Agree { get; set; }

        /// <summary>
        /// Không đồng tý với câu trả lời này
        /// </summary>
        public int Disagree { get; set; }

        /// <summary>
        /// Thời gian trả lời
        /// </summary>
        public DateTime ReplyDate { get; set; }
    }
}

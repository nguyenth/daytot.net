using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    public class Ask
    {
        /// <summary>
        /// Mã câu hỏi
        /// </summary>
        [Key]
        public int AskId { get; set; }

        /// <summary>
        /// Mã người comment
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Nội dung câu hỏi
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Description { get; set; }

        /// <summary>
        /// Tình trạng
        /// Bit 0: Open
        /// Bit 10: Đã đóng
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Mã tham chiều tới một đống tượng liên qua
        /// 0: là không tham chiếu tới đối tượng nào
        /// </summary>
        public int ReferId { get; set; }

        /// <summary>
        /// Loại đối tượng tham chiếu
        /// 0: Không tham chiếu
        /// </summary>
        public int ReferType { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Người comment
        /// </summary>
        [ForeignKey("UserId")]
        public User Asker { get; set; }

        /// <summary>
        /// Danh sách trả lời
        /// </summary>
        [ForeignKey("AskId")]
        public virtual ICollection<Reply> Replies { get; set; }

        /// <summary>
        /// Tags
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Tags { get; set; }

        #region properties helpers

        /// <summary>
        /// Trạng mở
        /// </summary>
        [NotMapped]
        public bool IsOpen
        {
            get{ return (Status & Consts.ASK_STATUS_OPEN) == Consts.ASK_STATUS_OPEN; }
            set{ Status = (value ? Status | Consts.ASK_STATUS_OPEN : (Status | Consts.ASK_STATUS_OPEN) ^ Status | Consts.ASK_STATUS_OPEN); }
        }

        /// <summary>
        /// Trạng thái đóng
        /// </summary>
        [NotMapped]
        public bool IsClosed
        {
            get { return (Status & Consts.ASK_STATUS_CLOSED) == Consts.ASK_STATUS_CLOSED; }
            set { Status = (value ? Status | Consts.ASK_STATUS_CLOSED : (Status | Consts.ASK_STATUS_CLOSED) ^ Status | Consts.ASK_STATUS_CLOSED); }
        }

        #endregion
    }
}

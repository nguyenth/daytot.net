using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    public class Enrollment
    {
        /// <summary>
        /// Mã đăng ký
        /// </summary>
        [Key]
        public int EnrollId {get; set;}

        /// <summary>
        /// Mã người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Mã khóa học
        /// </summary>
        public int CourseId { get; set; }
        
        /// <summary>
        /// Loại ghi danh:
        /// 0: Miễn phí
        /// 1: Trả phí
        /// </summary>
        public int Type { get; set; }
        
        /// <summary>
        /// Ngày ghi danh
        /// </summary>
        public DateTime EnrollDate { get; set; }

        /// <summary>
        /// Xóa
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Ngày xóa
        /// </summary>
        public DateTime? Deleted { get; set; }

        /// <summary>
        /// Hòa thành khóa học
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Ngày trả phí
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Mã hóa đơn. nếu trả phí
        /// </summary>
        public string InvoiceCode { get; set; }
        
        /// <summary>
        /// Thời gian học gần nhất
        /// </summary>
        public DateTime? LastLearn { get; set; }

        /// <summary>
        /// Danh sách thông tin học tập.
        /// </summary>
        [ForeignKey("EnrollId")]
        public ICollection<LearnActivity> Activities { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace daytot.core.models
{
    public class Section
    {
        /// <summary>
        /// Mã phần
        /// </summary>
        [Key]
        public int SectionId { get; set; }

        /// <summary>
        /// Mã khóa học
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Tên chương
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [JsonIgnore]
        public DateTime Created { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        [JsonIgnore]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Danh sách bài giảng
        /// </summary>
        [ForeignKey("SectionId")]
        public virtual ICollection<Lecture> Lectures { get; set; }

        #region properties helper

        /// <summary>
        /// Trạng thái chờ duyệt
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public bool IsWaitForApprove {
            get {
                return (Status & core.Consts.STATUS_WAITFORAPPROVE) == core.Consts.STATUS_WAITFORAPPROVE;
            }
            set
            {
                Status |= core.Consts.STATUS_WAITFORAPPROVE;
                if (!value)
                    Status ^= core.Consts.STATUS_WAITFORAPPROVE;
            }
        }

        /// <summary>
        /// Trạng thái nội dung được duyệt
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public bool IsApproved
        {
            get
            {
                return (Status & core.Consts.STATUS_APPROVED) == core.Consts.STATUS_APPROVED;
            }
            set
            {
                Status |= core.Consts.STATUS_APPROVED;
                if (!value)
                    Status ^= core.Consts.STATUS_APPROVED;
            }
        } 

        #endregion
    }
}

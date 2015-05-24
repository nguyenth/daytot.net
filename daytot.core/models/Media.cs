using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using daytot.core.contracts;

namespace daytot.core.models
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        /// <summary>
        /// Loại media
        /// </summary>
        public int ReferTypeId { get; set; }

        /// <summary>
        /// Mã liên quan Media thuộc về
        /// </summary>
        public int ReferId { get; set; }

        /// <summary>
        /// Loại thông tin đa phương tiện:
        /// 1: Hình (jpg,gif,png,…)
        /// 2: External Link hình.
        /// 3: Video upload.
        /// 4: External Link Video  Youtube
        /// 5: External Link Video  Vimeo
        /// </summary>
        public int MediaTypeId { get; set; }

        /// <summary>
        /// Cách sử dụng
        /// 1: Avatar
        /// 2: Course intro
        /// </summary>
        public int Usage { get; set; }

        /// <summary>
        /// Tiêu đề Media
        /// </summary>
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Title { get; set; }

        /// <summary>
        /// Thồi gian chạy cho file video
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public byte Sort { get; set; }

        /// <summary>
        /// Hình nằm trên server
        /// Giá trị = 0 là không dùng CDN
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// Đường dẫn file media
        /// </summary>
        [StringLength(256, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        [Column(TypeName = "varchar")]
        public string MediaUrl { get; set; }

        /// <summary>
        /// Tình trạng của tin đăng theo BitMask:
        /// 1: Chưa dùng
        /// 2: IsWaitForApprove - Chờ duyệt
        /// 3: IsApproved - Đã duyệt
        /// 4: IsDeleted - Đã xoá
        /// 5: IsRejected - Không duyệt
        /// 6-7: Chưa dùng
        /// 8: IsRepresentative - Media tiêu biểu được chọn
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        public int? UpdatedBy { get; set; }

        #region properties helper
        /// <summary>
        /// Trạng thái chờ duyệt
        /// </summary>
        [NotMapped]
        public bool IsWaitForApproved {
            get {
                return (Status & core.Consts.STATUS_WAITFORAPPROVE) == core.Consts.STATUS_WAITFORAPPROVE;
            }
            set {
                Status |= core.Consts.STATUS_WAITFORAPPROVE;
                if (!value){
                    Status |= core.Consts.STATUS_WAITFORAPPROVE;
                    Status ^= core.Consts.STATUS_WAITFORAPPROVE;
                }
            }
        }

        /// <summary>
        /// Trạng thái đã duyệt
        /// </summary>
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
                {
                    Status ^= core.Consts.STATUS_APPROVED;
                }
            }
        }

        /// <summary>
        /// Trạng thái xóa
        /// </summary>
        [NotMapped]
        public bool IsDeleted
        {
            get
            {
                return (Status & core.Consts.STATUS_DELETED) == core.Consts.STATUS_DELETED;
            }
            set
            {
                Status |= core.Consts.STATUS_DELETED;
                if (!value)
                {
                    Status ^= core.Consts.STATUS_DELETED;
                }
            }
        }

        /// <summary>
        /// Trạng thái xóa
        /// </summary>
        [NotMapped]
        public bool IsRepresentative
        {
            get
            {
                return (Status & core.Consts.STATUS_REPRESENTATIVE) == core.Consts.STATUS_REPRESENTATIVE;
            }
            set
            {
                Status |= core.Consts.STATUS_REPRESENTATIVE;
                if (!value)
                {
                    Status ^= core.Consts.STATUS_REPRESENTATIVE;
                }
            }
        }

        /// <summary>
        /// Đường dẫn view hình
        /// </summary>
        [NotMapped]
        public string PublishUrl {
            get
            {
                string defaultLocation = Consts.DEFAULT_INTRO_IMAGE;
                switch (Usage) {
                    case core.Consts.MEDIA_USAGE_AVATAR:
                        defaultLocation = Consts.DEFAULT_AVATAR_IMAGE;
                        break;
                    case core.Consts.MEDIA_USAGE_COURSEINTRO:
                        defaultLocation = Consts.DEFAULT_INTRO_IMAGE;
                        break;
                }

                string url = string.IsNullOrEmpty(MediaUrl) ? defaultLocation : MediaUrl;

                if (MediaTypeId != core.Consts.MEDIA_TYPE_YOUTUBE && 
                    MediaTypeId != core.Consts.MEDIA_TYPE_VIMEO && 
                    MediaTypeId != core.Consts.MEDIA_TYPE_EXTERNALIMAGE)
                    return System.IO.Path.Combine(@"/uploads", url);

                return url;
            }
        }
        #endregion
    }
}

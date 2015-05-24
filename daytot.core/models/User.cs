using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

using daytot.core.utils;

namespace daytot.core.models
{
    /// <summary>
    /// Lớp mô tả thông tin người dùng trên hệ thống
    /// </summary>
    public class User  {

        /// <summary>
        /// Mã số người dùng trên hệ thống
        /// </summary>
        [Key]
        public int UserId { get;set;}
        /// <summary>
        /// Họ & tên
        /// </summary>
        [Display(Name = "User_FullName", ResourceType=typeof(resources.Models))]
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string FullName { get; set; }

        /// <summary>
        /// Media hình đại diện dưới dạng Json
        /// </summary>
        [Display(Name = "User_Avatar", ResourceType = typeof(resources.Models))]
        public string AvatarObject { get; set; }

        /// <summary>
        /// Địa chỉ email cũng là tên đăng nhập
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(resources.Validations))]
        [Display(Name = "User_Email", ResourceType = typeof(resources.Models))]
        [StringLength(150, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Display(Name = "User_Phone", ResourceType = typeof(resources.Models))]
        [StringLength(15, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Phone { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        [Display(Name = "User_CardId", ResourceType = typeof(resources.Models))]
        [StringLength(12, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string CardId { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        [Display(Name = "User_IssuePlace", ResourceType = typeof(resources.Models))]
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string IssuePlace { get; set; }

        /// <summary>
        /// Địa chỉ Facebook
        /// </summary>
        [StringLength(250, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Facebook { get; set; }

        /// <summary>
        /// Địa chỉ Twitter
        /// </summary>
        [StringLength(250, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Twitter { get; set; }

        /// <summary>
        /// Địa chỉ google plus
        /// </summary>
        [StringLength(250, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string GooglePlus { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Display(Name = "User_Birthday", ResourceType = typeof(resources.Models))]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Nơi sinh
        /// </summary>
        [Display(Name = "User_PlaceOfBirth", ResourceType = typeof(resources.Models))]
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Ngày đăng nhập gần nhất
        /// </summary>
        [Display(Name = "User_LastLogin", ResourceType = typeof(resources.Models))]
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Loại tài khoản
        ///  - Bit 1: Học sinh
        ///  - Bit 2 - bit 10: chưa dùng
        ///  - Bit 11: Giáo viên
        ///  - Bit 12: Chủ nhiệm bộ môn
        ///  - Bit 13 - bit 31: chưa dùng
        /// </summary>
        [Display(Name = "User_Type", ResourceType = typeof(resources.Models))]
        public int Type { get; set; }

        /// <summary>
        /// Tình trạng tài khoản. xác định thông tin hoàn chỉnh 1 tài khoản
        ///  - Bit 1: Chưa cập nhật thông tin
        ///  - Bit 2: Đã xác nhận email
        ///  - Bit 3: Đã xác thực số điện thoại
        ///  - Bit 4: Đã xác thực CMND
        /// </summary>
        [Display(Name = "User_Status", ResourceType = typeof(resources.Models))]
        public int Status { get; set; }

        /// <summary>
        /// Mô tả bản thân
        /// </summary>
        [Display(Name = "User_Brief", ResourceType = typeof(resources.Models))]
        [StringLength(1024, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string Brief { get; set; }

        /// <summary>
        /// Trường học, nơi học sinh học tập/giáo viên giảng dạy
        /// </summary>
        [Display(Name = "User_School", ResourceType = typeof(resources.Models))]
        [StringLength(50, ErrorMessageResourceName = "InvalidMaxLength", ErrorMessageResourceType = typeof(resources.Validations))]
        public string School { get; set; }

        /// <summary>
        /// Xác thực một giáo viên
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Bộ môn phụ trách chính
        /// </summary>
        [Display(Name = "User_SubjectId", ResourceType = typeof(resources.Models))]
        public int? SubjectId { get; set; }

        /// <summary>
        /// Số connections liên quan tới User này
        /// </summary>
        [ForeignKey("UserId")]
        public ICollection<OnlineConnection> Connections { get; set; }

        #region properties helper
        /// <summary>
        /// Xác định có phải là tài khoản học viên
        /// </summary>
        [NotMapped]
        public bool IsStudent
        {
            get
            {
                return (Type & Consts.ACCOUN_TYPE_STUDENT) == Consts.ACCOUN_TYPE_STUDENT;
            }
        }

        /// <summary>
        /// Xác định có phải là tài khoản teacher
        /// </summary>
        [NotMapped]
        public bool IsTeacher {
            get {
                return (Type & Consts.ACCOUN_TYPE_TEACHER) == Consts.ACCOUN_TYPE_TEACHER;
            }
        }

        /// <summary>
        /// Xác định có phải là tài khoản trưởng bộ môn
        /// </summary>
        [NotMapped]
        public bool IsDean
        {
            get
            {
                return (Type & Consts.ACCOUN_TYPE_DEAN) == Consts.ACCOUN_TYPE_DEAN;
            }
        }

        /// <summary>
        /// Xác định có phải là tài khoản admin
        /// </summary>
        [NotMapped]
        public bool IsAdmin
        {
            get
            {
                return (Type & Consts.ACCOUN_TYPE_ADMIN) == Consts.ACCOUN_TYPE_ADMIN;
            }
        }

        /// <summary>
        /// Kiểm tra tình trạng kích hoạt
        /// </summary>
        [NotMapped]
        public bool IsActivate {
            get {
                return (Status & Consts.ACCOUN_STATUS_VERIFY_EMAIL) == Consts.ACCOUN_STATUS_VERIFY_EMAIL;
            }
        }

        /// <summary>
        /// Avatar
        /// </summary>
        [NotMapped]
        public Media Avatar {
            get {
                if (!string.IsNullOrEmpty(AvatarObject)) {
                    return AvatarObject.FromJson<Media>();
                }
                return null;
            }
            set {
                AvatarObject = value.ToJson();
            }
        }

        /// <summary>
        /// Đường dẫn hiển thị avatar
        /// </summary>
        public string GetAvatarDisplay()
        {
            return Avatar != null ? Avatar.PublishUrl : core.utils.Common.DefaultImage(core.Consts.MEDIA_USAGE_AVATAR);
        }

        #endregion

        #region helper methods
        /// <summary>
        /// Lấy thông tin Credential
        /// </summary>
        /// <returns></returns>
        public projectors.Credential GetCredential()  {
            return new projectors.Credential
            {
                UserId = UserId,
                UserName = Email,
                FullName = FullName,
                Avatar = GetAvatarDisplay(),
                Phone = Phone,
                IsActivate  = IsActivate,
                IsStudent= IsStudent,
                IsTeacher = IsTeacher,
                IsDean = IsDean,
                IsAdmin = IsAdmin
            };
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.utils;
using daytot.core.models;

namespace daytot.core.projectors
{
    public class UserSmall
    {
        /// <summary>
        /// Mã số người dùng trên hệ thống
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Họ & tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Media hình đại diện dưới dạng Json
        /// </summary>
        public string AvatarObject { get; set; }

        /// <summary>
        /// Địa chỉ email cũng là tên đăng nhập
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Địa chỉ Facebook
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// Địa chỉ Twitter
        /// </summary>
        public string Twitter { get; set; }

        /// <summary>
        /// Địa chỉ google plus
        /// </summary>
        public string GooglePlus { get; set; }

        /// <summary>
        /// Ngày đăng nhập gần nhất
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Loại tài khoản
        ///  - Bit 1: Học sinh
        ///  - Bit 2 - bit 10: chưa dùng
        ///  - Bit 11: Giáo viên
        ///  - Bit 12: Chủ nhiệm bộ môn
        ///  - Bit 13 - bit 31: chưa dùng
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Tình trạng tài khoản. xác định thông tin hoàn chỉnh 1 tài khoản
        ///  - Bit 1: Chưa cập nhật thông tin
        ///  - Bit 2: Đã xác nhận email
        ///  - Bit 3: Đã xác thực số điện thoại
        ///  - Bit 4: Đã xác thực CMND
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Bộ môn phụ trách chính
        /// </summary>
        public int? SubjectId { get; set; }

        public string AvatarUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(AvatarObject))
                {
                    var media = AvatarObject.FromJson<Media>();
                    return media.PublishUrl;
                }
                return null;

            }
        }
    }
}

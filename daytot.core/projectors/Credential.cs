using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.projectors
{

    /// <summary>
    /// Thông tin người sử  dụng đã đăng nhập
    /// </summary>
    public class Credential
    {
        /// <summary>
        /// Mã người dùng
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// tên người dùng
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get;  set; }
        /// <summary>
        /// Avatar
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Kiểm tra tình trạng kích hoạt
        /// </summary>
        public bool IsActivate { get; set; }

        /// <summary>
        /// tài khoản học sinh
        /// </summary>
        public bool IsStudent { get; set; }

        /// <summary>
        /// Giáo viên
        /// </summary>
        public bool IsTeacher { get; set; }
        /// <summary>
        /// Trưởng bộ môn
        /// </summary>
        public bool IsDean { get; set; }
        /// <summary>
        /// Admin
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Danh sách đối tượng yêu thích
        /// </summary>
        public ICollection<FavoriteExtraSmall> Favorites { get; set; }

        /// <summary>
        /// Lấy tên của người dùng
        /// </summary>
        public string FirstName {
            get {
                int lastSpace = FullName.LastIndexOf(" ");
                if (lastSpace >= 0)
                    return FullName.Substring(lastSpace);
                return FullName;
            }
        }
    }
}

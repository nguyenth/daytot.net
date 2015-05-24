using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.utils;
using daytot.core.models;

namespace daytot.core.projectors
{
    public class StudentSmall
    {
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Avatar
        /// </summary>
        public string AvatarObject { get; set; }
        
        public int CourseId { get; set; }
        /// <summary>
        /// Tên khóa học
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// Mã ghi danh
        /// </summary>
        public int EnrollId { get; set; }
        /// <summary>
        /// Loại ghi danh
        /// </summary>
        public int EnrollType { get; set; }
        /// <summary>
        /// Ngày ghi danh
        /// </summary>
        public DateTime EnrollDate { get; set; }
        /// <summary>
        /// Ngày học cuối cùng
        /// </summary>
        public DateTime? LastLearn { get; set; }
        /// <summary>
        /// Hoàn thành khóa học chay chưa
        /// </summary>
        public bool? IsCompleted { get; set; }

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

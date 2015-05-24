using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.utils;

namespace daytot.core.projectors
{
    public class CourseSmall
    {
        /// <summary>
        /// Mã khóa học
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Mã tài khoản chủ khóa học
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Cấp độ kiến thức
        /// </summary>
        public int ClassLevelId { get; set; }

        /// <summary>
        /// Môn học
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// Loại khóa học
        /// 0: Miễn phí
        /// 1: Tính phí
        /// 2: Khóa học riêng tư
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Tên khóa học
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// Thời gian học tính theo giờ
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Object hình ảnh giới thiệu lưu dưới dạng json
        /// </summary>
        public string IntroImage { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Tên giáo viên chủ khóa học
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Hình đại diện
        /// </summary>
        public string AvatarObject { get; set; }

        /// <summary>
        /// Số lần được xem.
        /// </summary>
        public int View { get; set; }

        /// <summary>
        /// Số học sinh tham gia.
        /// </summary>
        public int EnrollmentCounter { get; set; }


        /// <summary>
        /// Đối tượng hình giới thiệu
        /// </summary>
        
        public Media IntroImageMedia
        {
            get
            {
                if (!string.IsNullOrEmpty(IntroImage))
                    return IntroImage.FromJson<Media>();
                return new Media
                {
                    MediaId = 0,
                    MediaTypeId = Consts.MEDIA_TYPE_IMAGE,
                    MediaUrl = Consts.DEFAULT_INTRO_IMAGE,
                    ReferId = CourseId,
                    ReferTypeId = Consts.REFERTYPE_COURSE,
                    ServerId = 0
                };

            }
        }

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

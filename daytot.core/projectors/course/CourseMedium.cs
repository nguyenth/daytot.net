using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.utils;

namespace daytot.core.projectors
{
    public class CourseMedium
    {
        public CourseMedium()
        {
            Teachers = new List<Assignment>();
        }

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
        /// Object video giới thiệu lưu dưới dạng json
        /// </summary>
        public string IntroVideo { get; set; }

        /// <summary>
        /// Nội dung giới thiệu
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// Mô tả yêu cầu tối thiệu tham gia khóa học
        /// </summary>
        public string Requirement { get; set; }

        /// <summary>
        /// Mô tả cách thức học khóa học này
        /// </summary>
        public string HowToLearn { get; set; }

        /// <summary>
        /// Tóm lược mội dung khóa học
        /// </summary>
        public string Syllabus { get; set; }

        /// <summary>
        /// Tình trạng
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Chuổi tag phân cách nhau bằng dấu ","
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Học phí khóa học.
        /// Nếu khóa học là miễn phí(Type = 0) thì CourseFee sẽ bằng 0
        /// </summary>
        public decimal CourseFee { get; set; }

        /// <summary>
        /// Token gốc dùng tạo các mã tham gia khóa học cho thành viên.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Số lần được xem.
        /// </summary>
        public int View { get; set; }

        /// <summary>
        /// Số học sinh tham gia.
        /// </summary>
        public int EnrollmentCounter { get; set; }

        /// <summary>
        /// Danh sách giáo viên tham gia giảng dạy
        /// </summary>
        public virtual ICollection<Assignment> Teachers { get; set; }

        /// <summary>
        /// Nội dung khóa học
        /// </summary>
        public virtual ICollection<Section> Sections { get; set; }

        #region properties helper

        /// <summary>
        /// Đối tượng video giới thiệu
        /// </summary>
        public Media IntroVideoMedia {
            get
            {
                if (!string.IsNullOrEmpty(IntroImage))
                    return IntroVideo.FromJson<Media>();
                return null;
            }
            set {
                if (value != null)
                {
                    IntroVideo = value.ToJson();
                }
                else
                    IntroVideo = null;
            }
        }

        /// <summary>
        /// Đối tượng hình giới thiệu
        /// </summary>
        public Media IntroImageMedia
        {
            get
            {
                if(!string.IsNullOrEmpty(IntroImage))
                    return IntroImage.FromJson<Media>();
                return new Media { 
                    MediaId = 0,
                    MediaTypeId = Consts.MEDIA_TYPE_IMAGE,
                    MediaUrl = Consts.DEFAULT_INTRO_IMAGE,
                    ReferId = CourseId,
                    ReferTypeId = Consts.REFERTYPE_COURSE,
                    ServerId = 0 
                };

            }
            set
            {
                if (value != null)
                {
                    IntroImage = value.ToJson();
                }
                else
                    IntroImage = null;
            }
        }

        #endregion
    }
}

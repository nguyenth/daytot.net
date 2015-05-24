using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using daytot.core.utils;
using daytot.core.projectors;

namespace daytot.core.models
{
    public class Course
    {
        public Course()
        {
            Teachers = new List<Assignment>();
        }
        /// <summary>
        /// Mã khóa học
        /// </summary>
        [Key]
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
        [StringLength(150)]
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
        /// Khóa học có đăng được công khai hay không.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Chuổi tag phân cách nhau bằng dấu ","
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Dữ liệu chỉnh sửa
        /// </summary>
        public string DraftObject { get; set; }

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
        /// Địa chỉ IP cuối cùng cập nhật
        /// </summary>
        public string LastIP { get; set; }

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
        [ForeignKey("CourseId")]
        public virtual ICollection<Assignment> Teachers { get; set; }

        /// <summary>
        /// Nội dung khóa học
        /// </summary>
        [ForeignKey("CourseId")]
        public virtual ICollection<Section> Sections { get; set; }

        #region properties helper
        // <summary>
        /// Tình trạng đang soạn thảo
        /// </summary>
        [NotMapped]
        public bool IsInit
        {
            get
            {
                return (Status & core.Consts.STATUS_INIT) == core.Consts.STATUS_INIT;
            }
            set
            {
                Status |= core.Consts.STATUS_INIT;
                if (!value)
                    Status ^= core.Consts.STATUS_INIT;
            }
        }

        /// <summary>
        /// Tình trạng đang soạn thảo
        /// </summary>
        [NotMapped]
        public bool IsDraft
        {
            get
            {
                return (Status & core.Consts.STATUS_DRAFT) == core.Consts.STATUS_DRAFT;
            }
            set
            {
                Status |= core.Consts.STATUS_DRAFT;
                if (!value)
                    Status ^= core.Consts.STATUS_DRAFT;
            }
        }

        /// <summary>
        /// Tình trạng chờ duyệt
        /// </summary>
        [NotMapped]
        public bool IsWaitForApprove
        {
            get
            {
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
        /// Tình trạng đã duyệt nội dung
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
                    Status ^= core.Consts.STATUS_APPROVED;
            }
        }

        /// <summary>
        /// Tình trạng đang công khai
        /// </summary>
        [NotMapped]
        public bool IsPublished
        {
            get
            {
                return (Status & core.Consts.STATUS_PUBLISHED) == core.Consts.STATUS_PUBLISHED;
            }
            set
            {
                Status |= core.Consts.STATUS_PUBLISHED;
                if (!value)
                    Status ^= core.Consts.STATUS_PUBLISHED;
            }
        }

        /// <summary>
        /// Tình trạng đã xóa
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
                    Status ^= core.Consts.STATUS_DELETED;
            }
        }

        /// <summary>
        /// Đối tượng video giới thiệu
        /// </summary>
        [NotMapped]
        public Media IntroVideoMedia
        {
            get
            {
                if (!string.IsNullOrEmpty(IntroImage))
                    return IntroVideo.FromJson<Media>();
                return null;
            }
            set
            {
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
        [NotMapped]
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

        /// <summary>
        /// Truy cập tới các gái trị thuộc tính bằng tên của thuộc tính
        /// </summary>
        /// <param name="property">Tên thuộc tính</param>
        /// <returns></returns>
        [NotMapped]
        public object this[string property]
        {
            get
            {
                try
                {
                    System.Reflection.PropertyInfo info = this.GetType().GetProperty(property);
                    if (info != null)
                    {
                        return info.GetValue(this);
                    }
                }
                catch (Exception ex)
                {
                    helpers.Logger.Error(string.Format("Course[get->{0}]", property), ex);
                }
                return null;
            }
            set
            {
                try
                {
                    System.Reflection.PropertyInfo info = this.GetType().GetProperty(property);
                    info.SetValue(this, value);
                }
                catch (Exception ex)
                {
                    helpers.Logger.Error(string.Format("Course[set<-{0}]", property), ex);
                }
            }
        }
        #endregion

        #region methods helper

        /// <summary>
        /// Chuyển tin đăng ô tô này qua bản lưu cấn kiểm duyệt.
        /// </summary>
        /// <returns>Trả về đối tượng ghi nhận những thông tin cần kiểm duyệt</returns>
        public string ToDraft()
        {
            return new CourseDraft
           {
               CourseName = CourseName,
               IntroImage = IntroImage,
               IntroVideo = IntroVideo,
               Intro = Intro,
               Requirement = Requirement,
               HowToLearn = HowToLearn,
               Syllabus = Syllabus,
               Tags = Tags,
               Updated = Updated,
               UpdatedBy = UpdatedBy,
               LastIP = LastIP
           }.ToJson();
        }

        /// <summary>
        /// Chuyển tất dữ liệu thông tin chưa kiểm duyệt thanh dữ liệu đã kiểm duyệt & giữ lại dữ liệu duyệt đã lưu
        /// </summary>
        public void ApplyDraft()
        {
            if (string.IsNullOrEmpty(DraftObject)) return;

            var draft = DraftObject.FromJson<CourseDraft>();
            if (draft != null)
            {
                CourseName = draft.CourseName;
                IntroImage = draft.IntroImage;
                IntroVideo = draft.IntroVideo;
                Intro = draft.Intro;
                Requirement = draft.Requirement;
                HowToLearn = draft.HowToLearn;
                Syllabus = draft.Syllabus;
                Tags = draft.Tags;
                Updated = draft.Updated;
                UpdatedBy = draft.UpdatedBy;
                LastIP = draft.LastIP;
            }
        }

        /// <summary>
        /// Lấy thông tin dạng trung bình
        /// </summary>
        /// <returns></returns>
        public CourseMedium ToMedium()
        {
            return new CourseMedium
            {
                CourseId = CourseId,
                UserId = UserId,
                ClassLevelId = ClassLevelId,
                SubjectId = SubjectId,
                Type = Type,
                CourseName = CourseName,
                Time = Time,
                IntroImage = IntroImage,
                IntroVideo = IntroVideo,
                Intro = Intro,
                Requirement = Requirement,
                HowToLearn = HowToLearn,
                Syllabus = Syllabus,
                Created = Created,
                Tags = Tags,
                CourseFee = CourseFee,
                View = View,
                EnrollmentCounter = EnrollmentCounter,
                Teachers = Teachers,
                Sections = Sections
            };
        }

        /// <summary>
        /// Lấy thông tin công khai dạng ít
        /// </summary>
        /// <returns></returns>
        public dynamic ToExtraSmallPublish() {
            return new {
                CourseName = CourseName,
                Teachers = Teachers.Select(o=> new { o.Teacher.UserId, o.Teacher.FullName, o.Teacher.SubjectId, o.Teacher.Avatar  })
            };
        }

        #endregion

    }

    class CourseDraft
    {
        public string CourseName { get; set; }
        public string IntroImage { get; set; }
        public string IntroVideo { get; set; }
        public string Intro { get; set; }
        public string Requirement { get; set; }
        public string HowToLearn { get; set; }
        public string Syllabus { get; set; }
        public string Tags { get; set; }
        public DateTime? Updated { get; set; }
        public int? UpdatedBy { get; set; }
        public string LastIP { get; set; }
    }
}

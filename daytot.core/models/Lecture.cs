using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

using daytot.core.utils;

namespace daytot.core.models
{
    public class Lecture
    {
        /// <summary>
        /// Mã bài giảng
        /// </summary>
        [Key]
        public int LectureId { get; set; }

        /// <summary>
        /// Mã chương
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Tên bài giảng
        /// </summary>
        public string LectureTitle { get; set; }

        /// <summary>
        /// Bài giảng theo kiểu Video. là một Object Media dạng video lưu dưới dạng Json
        /// </summary>
        [JsonIgnore]
        public string VideoJson { get; set; }

        /// <summary>
        /// Trạng thái một bài giảng
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Nội dung tóm tắt kiến thức về bài giảng
        /// </summary>
        public string KnowledgeSummary { get; set; }

        /// <summary>
        /// Danh sách tài liệu tham khảo. là một Object Media lưu dưới dạng Json
        /// </summary>
        public string MaterialObject { get; set; }

        /// <summary>
        /// Danh sách bài tập
        /// </summary>
        public string HomeworkObject { get; set; }

        /// <summary>
        /// Nội dung chỉnh sửa & chờ duyệt.
        /// </summary>
        public string DraftObject { get; set; }

        #region properties helper

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
        /// Video của bài giảng dạng Object Media
        /// </summary>
        [NotMapped]
        public Media Video {
            get {
                if (!string.IsNullOrEmpty(VideoJson))
                    return VideoJson.FromJson<Media>();
                return null;
            }
            set {
                if (value != null)
                    VideoJson = value.ToJson();
                else
                    VideoJson = null;
            }
        }

        /// <summary>
        /// Danh sách bài tập của bài giảng dạng danh sách object Quiz
        /// </summary>
        [NotMapped]
        public Quiz Homework
        {
            get
            {
                if (!string.IsNullOrEmpty(HomeworkObject))
                    return HomeworkObject.FromJson<Quiz>();
                return null;
            }
            set
            {
                if (value != null)
                    HomeworkObject = value.ToJson();
                else
                    HomeworkObject = null;
            }
        }

        /// <summary>
        /// Video của bài giảng dạng Object Media
        /// </summary>
        [NotMapped]
        public List<Media> Materials
        {
            get
            {
                if (!string.IsNullOrEmpty(MaterialObject))
                    return MaterialObject.FromJson<List<Media>>();
                return new List<Media>();
            }
            set
            {
                if (value != null)
                    MaterialObject = value.ToJson();
                else
                    MaterialObject = null;
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
                        return info.GetValue(this);
                }
                catch (Exception ex)
                {
                    helpers.Logger.Error(string.Format("Lecture[get->{0}]", property), ex);
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
                    helpers.Logger.Error(string.Format("Lecture[set<-{0}]", property), ex);
                }
            }
        }

        #endregion

        #region methods helper
        /// <summary>
        /// Áp dụng phiên bản soạn thảo chưa duyệt nếu có.
        /// </summary>
        public void ApplyDraft() {
            if (!string.IsNullOrEmpty(DraftObject))
            {
                var draft = DraftObject.FromJson<DraftLecture>();

                LectureTitle = draft.LectureTitle;
                KnowledgeSummary = draft.KnowledgeSummary;
                VideoJson = draft.VideoJson;
                HomeworkObject = draft.HomeworkObject;

                DraftObject = null;
            }
        }

        /// <summary>
        /// Chuyển đối tượng sang phiên bản nháp.
        /// </summary>
        /// <returns></returns>
        public string ToDraft() {
            return new DraftLecture
            {
                LectureTitle = LectureTitle,
                KnowledgeSummary = KnowledgeSummary,
                VideoJson = VideoJson,
                HomeworkObject = HomeworkObject
            }.ToJson();
        }

        /// <summary>
        /// Publish thông tin đã công khai
        /// </summary>
        /// <returns></returns>
        public dynamic ToPublish() {
            return new
            {
                LectureId,
                LectureTitle,
                Homework = Homework != null? Homework.ToPublish(): null,
                KnowledgeSummary,
                Materials = Materials.Select(m => new { m.MediaId, m.MediaTypeId, m.Title, m.PublishUrl }),
                SectionId,
                Video
            };
        }

        #endregion

    }

    internal class DraftLecture {

        /// <summary>
        /// Tên bài giảng
        /// </summary>
        public string LectureTitle { get; set; }

        /// <summary>
        /// Video bài giảng
        /// </summary>
        public string VideoJson { get; set; }

        /// <summary>
        /// Kiến thức tóm lượt
        /// </summary>
        public string KnowledgeSummary { get; set; }

        /// <summary>
        /// Nội dung bài tập
        /// </summary>
        public string HomeworkObject { get; set; }

    }
}

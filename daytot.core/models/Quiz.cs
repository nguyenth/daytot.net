using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    /// <summary>
    /// Đề thi
    /// </summary>
    public class Quiz
    {
        /// <summary>
        /// Mã bài kiểm tra
        /// </summary>
        [Key]
        public int QuizId { get; set; }

        /// <summary>
        /// Tên bài thi
        /// </summary>
        [StringLength(150)]
        public string Title { get; set; }

        /// <summary>
        /// Loại bài thi
        /// 0: Đề thi
        /// 1: Bài tập theo bái giảng
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Mã môn học
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// Mã cấp lớp
        /// </summary>
        public int ClassLevelId { get; set; }

        /// <summary>
        /// Thời gian làm bài
        /// </summary>
        public int TimeUp { get; set; }

        /// <summary>
        /// Phân loại bài tóan
        /// </summary>
        public int? TopicId { get; set; }

        /// <summary>
        /// Mã tham chiếu
        /// </summary>
        public int? ReferId { get; set; }

        /// <summary>
        /// Loại tham chiếu
        /// </summary>
        public int? ReferTypeId { get; set; }

        /// <summary>
        /// Mức đô khó
        /// 1: Dễ
        /// 2: trung bình
        /// 3:Khó
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// Tag cho bài kiểm tra
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Tên trường ra đề
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Trạng thái bài thi.
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Danh sách câu hỏi
        /// </summary>
        [ForeignKey("QuizId")]
        public ICollection<QuizDetail> Questions{ get; set; }


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

        #region methods helper
        public dynamic ToPublish() {
            return new
            {
                QuizId,
                Title,
                SubjectId,
                ClassLevelId,
                Type,
                TimeUp,
                TopicId,
                ReferId,
                ReferTypeId,
                Questions = Questions != null? Questions.OrderBy(o=> Guid.NewGuid()).Select(o => o.Question.ToPublish()).ToList(): null
            };
        }
        #endregion
    }
}

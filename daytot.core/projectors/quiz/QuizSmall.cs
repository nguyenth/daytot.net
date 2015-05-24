using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.utils;

namespace daytot.core.projectors
{
    public class QuizSmall
    {
        /// <summary>
        /// Mã bài kiểm tra
        /// </summary>
        public int QuizId { get; set; }

        /// <summary>
        /// Tên bài thi
        /// </summary>
        public string Title { get; set; }

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
        /// Mức đô khó
        /// 1: Dễ
        /// 2: trung bình
        /// 3:Khó
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// Tên trường ra đề
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// Trạng thái bài thi.
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        /// Ngày tạ0
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Avatar người tạo.
        /// </summary>
        public string CreatedByAvatar { get; set; }

        /// <summary>
        /// Số câu hỏi trong bài thi
        /// </summary>
        public int QuestionCounter { get; set; }

        public string AvatarUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(CreatedByAvatar))
                {
                    var media = CreatedByAvatar.FromJson<Media>();
                    return media.PublishUrl;
                }
                return null;

            }
        }

    }
}

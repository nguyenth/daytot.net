using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.projectors
{
    public class QuizCompleteSmall
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
        /// Lấn cuối gửi bài
        /// </summary>
        public DateTime? LastSubmit { get; set; }

        /// <summary>
        /// Số điểm
        /// </summary>
        public int Scores { get; set; }

        /// <summary>
        /// Số lần làm lại
        /// </summary>
        public int Attemps { get; set; }
    }
}

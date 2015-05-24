using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace daytot.core.models
{
    /// <summary>
    /// Chi tiết một bài thi
    /// </summary>
    public class QuizDetail
    {
        /// <summary>
        /// Mã bài thi
        /// </summary>
        public int QuizId { get; set; }

        /// <summary>
        /// Mã câu hỏi
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Câu hỏi
        /// </summary>
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

    }
}

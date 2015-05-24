using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.models
{
    /// <summary>
    /// Đáp án
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Mã đáp án
        /// </summary>
        [Key]
        public int AnswerId { get; set; }
        /// <summary>
        /// Nội dung đáp án
        /// </summary>
        public string AnswerContent { get; set; }
        /// <summary>
        /// Đáp án đúng
        /// </summary>
        public bool IsCorrectly { get; set; }

        /// <summary>
        /// Mã câu hỏi
        /// </summary>
        public int QuestionId { get; set; }

        #region methoads helper
        public dynamic ToPublish() {
            return new { 
                AnswerId,
                AnswerContent,
                QuestionId
            };
        }
        #endregion
    }
}

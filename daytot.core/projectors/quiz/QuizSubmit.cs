using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using daytot.core.utils;
using daytot.core.models;

namespace daytot.core.projectors
{
    
    public class QuizSubmit
    {
        /// <summary>
        /// Mã câu hỏi
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// Mã câu hỏi đúng
        /// </summary>
        public int CorrectAnswer { get; set; }
        /// <summary>
        /// Mã câu hỏi đã chọn
        /// </summary>
        public int? ChooseAnswer { get; set; }

        /// <summary>
        /// Gợi ý
        /// </summary>
        public string HintObject { get; set; }

        /// <summary>
        /// Đáp án đúng
        /// </summary>
        public bool IsCorrectly
        {
            get { return CorrectAnswer == ChooseAnswer; }
        }

        public virtual ICollection<Hint> Hints
        {
            get
            {
                if (!string.IsNullOrEmpty(HintObject))
                {
                    return HintObject.FromJson<List<Hint>>();
                }
                return new List<Hint>();
            }
        }
    }
}

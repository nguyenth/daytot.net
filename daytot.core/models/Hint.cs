using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.models
{
    /// <summary>
    /// Gợi ý
    /// </summary>
    public class Hint
    {
        /// <summary>
        /// Mã gợi ý
        /// </summary>
        public int HintId { get; set; }

        /// <summary>
        /// Bước gợi ý
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// Nội dung gợi ý
        /// </summary>
        public string HintContent { get; set; }

        /// <summary>
        /// Mã câu hỏi
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Tham chiếu tới nội dung kiến thức, bài giảng hiện có trên hệ thống
        /// </summary>
        public string References { get; set; }
    }
}

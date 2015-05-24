using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace daytot.core.models
{
    public class QuizActivity
    {
        /// <summary>
        /// Mã người sử dụng
        /// </summary>
        [Column(Order = 1), Key]
        public int UserId { get; set; }

        /// <summary>
        /// Mã bài thi
        /// </summary>
        [Column(Order = 2), Key]
        public int QuizId { get; set; }

        /// <summary>
        /// Lần thực hiện bài tập gần nhất
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Thời gian làm bài tình theo giây
        /// </summary>
        public int TotalTime { get; set; }

        /// <summary>
        /// Thời gian nộp bài tập này gần nhất
        /// </summary>
        public DateTime? LastSubmit { get; set; }

        /// <summary>
        /// Dữ liệu submit khi làm bài lần cuối.
        /// Lưu dưới dạng json: List<QuizSubmit>
        /// </summary>
        public string LastSubmitData { get; set; }

        /// <summary>
        /// Điểm số gần nhất
        /// </summary>
        public int Scores { get; set; }

        /// <summary>
        /// Số điểm bị trừ khi sử dụng các công cụ gợi ý.
        /// </summary>
        public int MinusScores{get; set;}

        /// <summary>
        /// Số lần làm bài tập này
        /// </summary>
        public int Attemps { get; set; }


    }
}

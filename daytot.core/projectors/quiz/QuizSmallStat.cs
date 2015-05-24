using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.projectors
{
    public class QuizSmallStat
    {
        /// <summary>
        /// Mã lớp
        /// </summary>
        public int ClassLevelId { get; set; }

        /// <summary>
        /// Mã môn học
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// Tổng số đề thi
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Tổng số đề thi đang công khai
        /// </summary>
        public int PublishedQuizes { get; set; }

        /// <summary>
        /// Tổng số đề thi đang chờ dyệt
        /// </summary>
        public int WaitForApproveQuizes { get; set; }

        /// <summary>
        /// Tổng số đề thi đang soạn
        /// </summary>
        public int DraftQuizes { get; set; }
    }
}

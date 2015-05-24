using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.projectors
{
    public class LearnActivitySmall
    {
        public int EnrollId { get; set; }

        public int LectureId { get; set; }
        public string LectureTitle { get; set; }
        public int SpendTime { get; set; }
        public int Completed { get; set; }
        public DateTime? LastView { get; set; }
        public int Attemps { get; set; }
    }
}

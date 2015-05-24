using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.projectors
{
    public class PagingResult<T> where T: class
    {
        public PagingResult() {
            Total = 0;
            Listing = new List<T>();
        }
        /// <summary>
        /// Danh sách đối tượng
        /// </summary>
        public List<T> Listing { get; set; }

        /// <summary>
        /// Tổng số thỏa điều kiện
        /// </summary>
        public int Total { get; set; }
    }
}

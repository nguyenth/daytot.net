using System;
using System.Collections.Generic;
using System.Linq;

namespace daytot.core.projectors
{
    public class StatusResponse<T>
    {
        /// <summary>
        /// Tình trạng xử lý
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public T Data { get; set; }
    }
}
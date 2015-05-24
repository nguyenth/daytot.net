using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IReplyRepository: IBaseRepository<Reply>
    {
        /// <summary>
        /// rate cho một câu trả lời
        /// </summary>
        /// <param name="replyId">Mã câu trả lời</param>
        void Rate(int replyId, int rate);
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class ReplyRepository: BaseRepository<Reply>, IReplyRepository
    {
        public ReplyRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public void Rate(int replyId, int rate)
        {
            throw new NotImplementedException();
        }
    }
}

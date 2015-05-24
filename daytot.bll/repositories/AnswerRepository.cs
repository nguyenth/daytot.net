using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.core.models;
using daytot.core.contracts;

namespace daytot.bll.repositories
{
    public class AnswerRepository: BaseRepository<Answer>,  IAnswerRepository
    {
        public AnswerRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #region caching
        


        #endregion

    }
}

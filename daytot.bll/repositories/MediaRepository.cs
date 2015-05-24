using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class MediaRepository: BaseRepository<Media>, IMediaRepository
    {
        public MediaRepository(DbContext dbContext) : base(dbContext) { 
        }

        /// <summary>
        /// Xóa các Media theo một ReferId & ReferTypeId
        /// </summary>
        /// <param name="referId">Mã tham chiếu</param>
        /// <param name="referTypeId">Loại mã tham chiếu</param>
        public void Remove(int referId, int referTypeId) {
            var medias = _dbSet.Where(o => o.ReferId == referId && o.ReferTypeId == referTypeId);
            foreach (var o in medias) {
                o.IsDeleted = true;
                Update(o);
            }

        }
    }
}

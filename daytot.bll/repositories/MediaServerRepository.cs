using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class MediaServerRepository: BaseRepository<MediaServer>, IMediaServerRepository
    {
        public MediaServerRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Lấy một server lưu trữ
        /// </summary>
        /// <returns></returns>
        public MediaServer GetServer() {
            return _dbSet.AsNoTracking().OrderBy(o => new Guid()).Take(1).SingleOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(DbContext dbContext):base(dbContext) { 
        }

        /// <summary>
        /// Lưu một đối tượng vào danh mục yêu thích
        /// </summary>
        /// <param name="fav"></param>
        /// <returns></returns>
        public bool Save(Favorite fav) {
            var existed_fav = _dbSet.Any(o=>o.UserId == fav.UserId && o.ReferId == fav.ReferId && o.ReferTypeId == fav.ReferTypeId);
            if (!existed_fav)
            {
                fav.Created = DateTime.Now;
                Add(fav);
                _uow.Commit();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Xóa một đối tượng ra khỏi danh mục yêu thích
        /// </summary>
        /// <param name="fav"></param>
        /// <returns></returns>
        public bool Remove(Favorite fav)
        {
            var existed_fav = Get(fav.UserId, fav.ReferId, fav.ReferTypeId);
            Delete(existed_fav);
            _uow.Commit();
            return true;
        }

        /// <summary>
        /// Lấy danh sách đối tượng yêu thích của một tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<Favorite> GetByUser(int userId) {
            return _dbSet.AsNoTracking().Where(o=>o.UserId == userId);
        }

    }
}

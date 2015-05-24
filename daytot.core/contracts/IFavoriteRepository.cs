using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IFavoriteRepository: IBaseRepository<Favorite>
    {
        /// <summary>
        /// Lưu một đối tượng vào danh mục yêu thích
        /// </summary>
        /// <param name="fav"></param>
        /// <returns></returns>
        bool Save(Favorite fav);

        /// <summary>
        /// Xóa một đối tượng ra khỏi danh mục yêu thích
        /// </summary>
        /// <param name="fav"></param>
        /// <returns></returns>
        bool Remove(Favorite fav);

        /// <summary>
        /// Lấy danh sách đối tượng yêu thích của một tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IQueryable<Favorite> GetByUser(int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core.contracts
{
    public interface IBaseRepository<T> where T: class
    {
        /// <summary>
        /// Lấy tất cả danh sách thông tin T trong database
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();


        /// <summary>
        /// Lấy một Entity bằng Id kiểu int
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(params object[] ids);

        /// <summary>
        /// Tạo một Entity trong DB
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Cập nhật một Entity trong DB
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Xóa một Entity trong DB
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Xóa một Entity bằng Id của nó.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(int id);

        /// <summary>
        /// Xóa nhiều records
        /// </summary>
        /// <param name="lstEntity"></param>
        void Deletes(List<T> lstEntity);
    }
}

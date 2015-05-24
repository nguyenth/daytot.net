using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.projectors;

namespace daytot.core.contracts
{
    public interface IAskRepository: IBaseRepository<Ask>
    {
        /// <summary>
        /// Lấy danh sách câu hỏi của một User
        /// </summary>
        /// <param name="credential">Tài khoản người sử dụng</param>
        /// <param name="total">Tổng câu hỏi</param>
        /// <param name="curentPage">Trang cần lấy</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang</param>
        /// <returns></returns>
        IQueryable<Ask> GetAskeds(Credential credential, ref int total, int curentPage, int pageSize);

        /// <summary>
        /// Lấy danh sách câu hỏi user đã trả lời
        /// </summary>
        /// <param name="UserId">User's Id</param>
        /// <param name="total">Tổng câu hỏi</param>
        /// <param name="curentPage">Trang cần lấy</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang</param>
        /// <returns></returns>
        IQueryable<Ask> GetAskWasReplied(int userId, ref int total, int curentPage, int pageSize);

        /// <summary>
        /// Đóng một câu hỏi
        /// </summary>
        /// <param name="askId"></param>
        void Close(int askId);

        /// <summary>
        /// Lấy danh sách câu hỏi theo một loại đối tượng liên quan
        /// </summary>
        /// <param name="referId">Mã đó tuông</param>
        /// <param name="referType">Loại đối tượng</param>
        /// <returns></returns>
        IQueryable<Ask> GetByReferId(int referId, int referType, ref int total, int currenPage = 1, int pageSize = 10);


    }
}

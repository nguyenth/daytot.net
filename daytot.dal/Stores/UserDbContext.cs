using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace daytot.dal
{
    public partial class DaytotDbContext
    {
        #region Course StoreProcedure

        /// <summary>
        /// Lấy danh sách tài khoản theo điều kiện
        /// </summary>
        /// <param name="total">Tổng khóa học thỏa điều kiện</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số khóa học trên 1 trang</param>
        /// <param name="type">Loại khóa học</param>
        /// <param name="subjectId">Môn học</param>
        /// <param name="status">Tình trạng kích hoạt</param>
        /// <param name="tags">Nội dung</param>
        /// <param name="email">email</param>
        /// <returns></returns>
        public virtual List<core.projectors.UserSmall> dspUsers_Listing(ref int total, int currentPage, int pageSize,
            int type, int subjectId, long status, string email)
        {
            var pTotal = new SqlParameter("Total", total);
            var pCurrentPage = new SqlParameter("CurrentPage", currentPage);
            var pPageSize = new SqlParameter("PageSize", pageSize);
            var pType = new SqlParameter("Type", type);
            var pSubjectId = new SqlParameter("SubjectId", subjectId);
            var pStatus = new SqlParameter("Status", status);
            var pEmail = new SqlParameter("Email", email == null? "": email);

            pTotal.Direction = System.Data.ParameterDirection.Output;
            pTotal.SqlDbType = System.Data.SqlDbType.Int;

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.UserSmall>(@"dspUsers_Listing @Total OUT,
                                                                                                                        @CurrentPage,
                                                                                                                        @PageSize, 
                                                                                                                        @Type, 
                                                                                                                        @SubjectId, 
                                                                                                                        @Status, 
                                                                                                                        @Email",
                                                                                                                        pTotal,
                                                                                                                        pCurrentPage,
                                                                                                                        pPageSize,
                                                                                                                        pType,
                                                                                                                        pSubjectId,
                                                                                                                        pStatus,
                                                                                                                        pEmail);

            var list = ret.ToList();
            total = Convert.ToInt32(pTotal.Value);

            return list;

        }
        #endregion
    }
}

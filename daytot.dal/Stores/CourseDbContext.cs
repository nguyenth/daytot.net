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
        /// Lấy danh sách khóa học theo điều kiện
        /// </summary>
        /// <param name="total">Tổng khóa học thỏa điều kiện</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số khóa học trên 1 trang</param>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="classLevelId">Lớp</param>
        /// <param name="subjectId">Môn học</param>
        /// <param name="type">Loại khóa học</param>
        /// <param name="status">Tình trạng</param>
        /// <param name="tags">Nội dung</param>
        /// <returns></returns>
        public virtual List<core.projectors.CourseSmall> dspCourses_Listing(ref int total, int currentPage, int pageSize, 
            int courseId, int classLevelId, int subjectId, int type, long status, bool isPublicOnly, string tags)
        {
            var pTotal = new SqlParameter("Total", total);
            var pCurrentPage = new SqlParameter("CurrentPage", currentPage);
            var pPageSize = new SqlParameter("PageSize", pageSize);
            var pCourseId = new SqlParameter("CourseId", courseId);
            var pClassLevelId = new SqlParameter("ClassLevelId", classLevelId);
            var pSubjectId = new SqlParameter("SubjectId", subjectId);
            var pType = new SqlParameter("Type", type);
            var pStatus = new SqlParameter("Status", status);
            var pIsPublicOnly = new SqlParameter("IsPublicOnly", isPublicOnly);
            var pTags = new SqlParameter("Tags", tags);

            pTotal.Direction = System.Data.ParameterDirection.Output;
            pTotal.SqlDbType = System.Data.SqlDbType.Int;

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.CourseSmall>(@"dspCourses_Listing @Total OUT,
                                                                                                                        @CurrentPage,
                                                                                                                        @PageSize, 
                                                                                                                        @CourseId, 
                                                                                                                        @ClassLevelId, 
                                                                                                                        @SubjectId, 
                                                                                                                        @Type, 
                                                                                                                        @Status,
                                                                                                                        @IsPublicOnly, 
                                                                                                                        @Tags",
                                                                                                                        pTotal,
                                                                                                                        pCurrentPage,
                                                                                                                        pPageSize,
                                                                                                                        pCourseId,
                                                                                                                        pClassLevelId,
                                                                                                                        pSubjectId,
                                                                                                                        pType,
                                                                                                                        pStatus,
                                                                                                                        pIsPublicOnly,
                                                                                                                        pTags);

            var list = ret.ToList();
            total = Convert.ToInt32(pTotal.Value);

            return list;

        }

        /// <summary>
        /// Lấy danh sách khóa học tiêu biểu
        /// </summary>
        /// <returns></returns>
        public virtual List<core.projectors.CourseSmall> dspCourses_Top()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.CourseSmall>(@"dspCourses_Top").ToList();
        }

        /// <summary>
        /// Lấy danh sách khóa học một học sinh đăng ký
        /// </summary>
        /// <param name="total"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual List<core.projectors.CourseSmall> dspCourses_EnrolledListing(ref int total, int currentPage, int pageSize, 
            int userId)
        {
            var pTotal = new SqlParameter("Total", total);
            var pCurrentPage = new SqlParameter("CurrentPage", currentPage);
            var pPageSize = new SqlParameter("PageSize", pageSize);
            var pUserId = new SqlParameter("UserId", userId);

            pTotal.Direction = System.Data.ParameterDirection.Output;
            pTotal.SqlDbType = System.Data.SqlDbType.Int;

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.CourseSmall>(@"dspCourses_EnrolledListing @Total OUT,
                                                                                                                        @CurrentPage,
                                                                                                                        @PageSize, 
                                                                                                                        @UserId",
                                                                                                                        pTotal,
                                                                                                                        pCurrentPage,
                                                                                                                        pPageSize,
                                                                                                                        pUserId);

            var list = ret.ToList();
            total = Convert.ToInt32(pTotal.Value);

            return list;

        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

using daytot.core.projectors;

namespace daytot.dal
{
    public partial class DaytotDbContext
    {
        /// <summary>
        /// Kiểm tra ghi danh của một bài giảng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        public virtual int dspEnrollments_CheckLectureInEnroll(int userId, int lectureId)
        {
            var pUserId = new SqlParameter("UserId", userId);
            var pLectureId = new SqlParameter("LectureId", lectureId);

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<int>(@"dspEnrollments_CheckLectureInEnroll @UserId,
                                                                                                                        @LectureId",
                                                                                                                         pUserId,
                                                                                                                         pLectureId);

            return ret.SingleOrDefault();

        }

        /// <summary>
        /// Lấy dánh sách người dùng tham gia 1 khóa học.
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <returns></returns>
        public List<StudentSmall> dspLearnActivities_GetStudents(int courseId, int lectureId, bool connected, ref int total, int currentPage, int pageSize)
        {
            var pTotal = new SqlParameter("Total", total);
            var pCurrentPage = new SqlParameter("CurrentPage", currentPage);
            var pPageSize = new SqlParameter("PageSize", pageSize);
            var pCourseId = new SqlParameter("CourseId", courseId);
            var pLectureId = new SqlParameter("LectureId", lectureId);
            var pConnected = new SqlParameter("Connected", connected);

            pTotal.Direction = System.Data.ParameterDirection.Output;
            pTotal.SqlDbType = System.Data.SqlDbType.Int;

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<StudentSmall>(@"dspLearnActivities_GetStudents @Total OUT,
                                                                                                                        @CurrentPage,
                                                                                                                        @PageSize,
                                                                                                                        @CourseId, 
                                                                                                                        @LectureId,
                                                                                                                        @Connected",
                                                                                                                        pTotal,
                                                                                                                        pCurrentPage,
                                                                                                                        pPageSize,
                                                                                                                        pCourseId,
                                                                                                                        pLectureId,
                                                                                                                        pConnected);
            var list = ret.ToList();
            total = Convert.ToInt32(pTotal.Value);

            return list;
        }

        /// <summary>
        /// Lấy danh sách học của một ghi danh
        /// </summary>
        /// <param name="enrollId"></param>
        /// <returns></returns>
        public List<LearnActivitySmall> dspLearnActivities_GetLearnActivitiesByEnrollId(int enrollId)
        {
            var pEnrollId = new SqlParameter("EnrollId", enrollId);
            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<LearnActivitySmall>(@"dspLearnActivities_GetLearnActivitiesByEnrollId @EnrollId",
                                                                                                                       pEnrollId);
            return ret.ToList();
        }
        
    }
}

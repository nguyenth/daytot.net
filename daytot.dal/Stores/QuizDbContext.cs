using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

using daytot.core.projectors;

namespace daytot.dal
{
    public partial class DaytotDbContext
    {

        /// <summary>
        /// Thống kê đề thi theo lớp & môn học
        /// </summary>
        /// <returns></returns>
        public virtual List<core.projectors.QuizSmallStat> dspQuizs_GetSmallStatByClassAndSubject()
        {
            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.QuizSmallStat>(@"dspQuizs_GetSmallStatByClassAndSubject");
            return ret.ToList();
        }

        /// <summary>
        /// Lấy danh sách đề thi
        /// </summary>
        /// <param name="total"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="classLevelId"></param>
        /// <param name="subjectId"></param>
        /// <param name="topidId"></param>
        /// <param name="timeUp"></param>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public virtual List<core.projectors.QuizSmall> dspQuizs_GetAllExam(ref int total, int currentPage = 1, int pageSize = 30,
            int classLevelId = 0, int subjectId = 0, int topidId = 0, int timeUp = 0, int difficulty = 0, long status = 0)
        {
            var pTotal = new SqlParameter("Total", total);
            var pCurrentPage = new SqlParameter("CurrentPage", currentPage);
            var pPageSize = new SqlParameter("PageSize", pageSize);
            var pClassLevelId = new SqlParameter("ClassLevelId", classLevelId);
            var pSubjectId = new SqlParameter("SubjectId", subjectId);
            var pTopicId = new SqlParameter("TopicId", topidId);
            var pTimeUp = new SqlParameter("TimeUp", timeUp);
            var pDifficulty = new SqlParameter("Difficulty", difficulty);
            var pStatus = new SqlParameter("Status", status);

            pTotal.Direction = System.Data.ParameterDirection.Output;
            pTotal.SqlDbType = System.Data.SqlDbType.Int;

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.QuizSmall>(@"dspQuizs_GetAllExam @Total OUT,
                                                                                                                        @CurrentPage,
                                                                                                                        @PageSize, 
                                                                                                                        @ClassLevelId, 
                                                                                                                        @SubjectId, 
                                                                                                                        @TopicId,
                                                                                                                        @TimeUp,
                                                                                                                        @Difficulty,
                                                                                                                        @Status",
                                                                                                                        pTotal,
                                                                                                                        pCurrentPage,
                                                                                                                        pPageSize,
                                                                                                                        pClassLevelId,
                                                                                                                        pSubjectId,
                                                                                                                        pTopicId,
                                                                                                                        pTimeUp,
                                                                                                                        pDifficulty,
                                                                                                                        pStatus);

            var list = ret.ToList();
            total = Convert.ToInt32(pTotal.Value);

            return list;
        }

        /// <summary>
        /// Tính toán kết quả một bài thi
        /// </summary>
        /// <param name="quizId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual List<core.projectors.QuizSubmit> dspQuizs_CalcScore(int quizId, string result)
        {
            var pQuizId = new SqlParameter("QuizId", quizId);
            var pResult = new SqlParameter("Results", result);

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.QuizSubmit>(@"dspQuizs_CalcScore @QuizId,
                                                                                                                        @Results",
                                                                                                                        pQuizId,
                                                                                                                        pResult);
            return ret.ToList();
        }

        /// <summary>
        /// Lấy danh sách đề thi
        /// </summary>
        /// <param name="total"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="classLevelId"></param>
        /// <param name="subjectId"></param>
        /// <param name="topidId"></param>
        /// <param name="timeUp"></param>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public virtual List<QuizCompleteSmall> dspQuizs_GetUserQuiz(ref int total, int currentPage = 1, int pageSize = 30,
            int userId = 0, int classLevelId = 0, int subjectId = 0, int topidId = 0, int timeUp = 0)
        {
            var pTotal = new SqlParameter("Total", total);
            var pCurrentPage = new SqlParameter("CurrentPage", currentPage);
            var pPageSize = new SqlParameter("PageSize", pageSize);
            var pUserId = new SqlParameter("UserId", userId);
            var pClassLevelId = new SqlParameter("ClassLevelId", classLevelId);
            var pSubjectId = new SqlParameter("SubjectId", subjectId);
            var pTopicId = new SqlParameter("TopicId", topidId);
            var pTimeUp = new SqlParameter("TimeUp", timeUp);
            

            pTotal.Direction = System.Data.ParameterDirection.Output;
            pTotal.SqlDbType = System.Data.SqlDbType.Int;

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.projectors.QuizCompleteSmall>(@"dspQuizs_GetUserQuiz @Total OUT,
                                                                                                                        @CurrentPage,
                                                                                                                        @PageSize, 
                                                                                                                        @UserId, 
                                                                                                                        @ClassLevelId, 
                                                                                                                        @SubjectId, 
                                                                                                                        @TopicId,
                                                                                                                        @TimeUp",
                                                                                                                        pTotal,
                                                                                                                        pCurrentPage,
                                                                                                                        pPageSize,
                                                                                                                        pUserId,
                                                                                                                        pClassLevelId,
                                                                                                                        pSubjectId,
                                                                                                                        pTopicId,
                                                                                                                        pTimeUp);

            var list = ret.ToList();
            total = Convert.ToInt32(pTotal.Value);

            return list;
        }
        
    }
}

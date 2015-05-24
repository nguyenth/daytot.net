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

        /// <summary>
        /// Lấy danh sách câu hỏi theo điều kiện & phân trang
        /// </summary>
        /// <param name="total">Tổng số câu hỏi thòa điều kiện. được trả về sau khi thực hiện hàm</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang cần lấy</param>
        /// <param name="classLevelId">Lớp</param>
        /// <param name="subjectId">Môn học</param>
        /// <param name="topicId">Chuyên đề</param>
        /// <returns></returns>
        public virtual List<core.models.Question> dspQuestions_GetAll(ref int total, int currentPage, int pageSize,
            int questionId, int classLevelId, int subjectId, int topicId)
        {
            var pTotal = new SqlParameter("Total", total);
            var pCurrentPage = new SqlParameter("CurrentPage", currentPage);
            var pPageSize = new SqlParameter("PageSize", pageSize);
            var pQuestionId = new SqlParameter("QuestionId", questionId);
            var pClassLevelId = new SqlParameter("ClassLevelId", classLevelId);
            var pSubjectId = new SqlParameter("SubjectId", subjectId);
            var pTopicId = new SqlParameter("TopicId", topicId);

            pTotal.Direction = System.Data.ParameterDirection.Output;
            pTotal.SqlDbType = System.Data.SqlDbType.Int;

            var ret = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<core.models.Question>(@"dspQuestions_GetAll @Total OUT,
                                                                                                                        @CurrentPage,
                                                                                                                        @PageSize, 
                                                                                                                        @QuestionId,
                                                                                                                        @ClassLevelId, 
                                                                                                                        @SubjectId, 
                                                                                                                        @TopicId",
                                                                                                                        pTotal,
                                                                                                                        pCurrentPage,
                                                                                                                        pPageSize,
                                                                                                                        pQuestionId,
                                                                                                                        pClassLevelId,
                                                                                                                        pSubjectId,
                                                                                                                        pTopicId);

            var list = ret.ToList();
            total = Convert.ToInt32(pTotal.Value);

            return list;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class TopicRepository:BaseRepository<Topic>,  ITopicRepository
    {
        public TopicRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Lấy danh sách các chuyên đề theo một Lớp & Môn học
        /// </summary>
        /// <param name="classLevelId">Mã Lớp</param>
        /// <param name="subjectId">Mã môn học</param>
        /// <returns></returns>
        public IQueryable<Topic> Listing(int classLevelId, int subjectId)
        {
            return _dbSet.AsNoTracking().Where(o=>o.ClassLevelId == classLevelId && o.SubjectId == subjectId);
        }

        /// <summary>
        /// Số câu hỏi trong một chủ đề
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public int QuestionCouter(int topicId) {
            return _dbContext.Set<Question>().Where(o => o.TopicId == topicId).Count();
        }

    }
}

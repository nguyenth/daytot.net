using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface ITopicRepository: IBaseRepository<Topic>
    {
        /// <summary>
        /// Lấy danh sách các chuyên đề theo một Lớp & Môn học
        /// </summary>
        /// <param name="classLevelId">Mã Lớp</param>
        /// <param name="subjectId">Mã môn học</param>
        /// <returns></returns>
        IQueryable<Topic> Listing(int classLevelId, int subjectId);

        // <summary>
        /// Số câu hỏi trong một chủ đề
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        int QuestionCouter(int topicId);
    }
}

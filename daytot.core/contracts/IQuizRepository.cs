using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.projectors;

namespace daytot.core.contracts
{
    public interface IQuizRepository: IBaseRepository<Quiz>
    {
        /// <summary>
        /// Lấy câu hỏi đầu đủ với thông tin câu hỏi
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        Quiz GetFull(int quizId);
        
        /// <summary>
        /// Lấy một bài tập của một Lecture
        /// </summary>
        /// <param name="lectureId">Mã bà giảng(Lecture's Id)</param>
        /// <returns></returns>
        Quiz GetByLecture(int lectureId);

        /// <summary>
        /// Thống kê số lượng đề thi theo lớp & môn học
        /// </summary>
        /// <returns></returns>
        List<QuizSmallStat> GetSmallStatByClassAndSubject();

        /// <summary>
        /// Lấy danh sách đề thi
        /// </summary>
        /// <param name="total">Tổng đề thi thỏa điều kiện</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số đề thi trên trang</param>
        /// <param name="classLevelId">Mã lớp</param>
        /// <param name="subjectId">Mã môn học</param>
        /// <param name="topidId">Chuyên đề</param>
        /// <param name="timeUp">Thời gian thi</param>
        /// <param name="difficulty">Độ khó</param>
        /// <param name="status">Trạng thái tổ hợp</param>
        /// <returns></returns>
        List<QuizSmall> GetAll(ref int total, int currentPage = 1, int pageSize = 30,
            int classLevelId = 0, int subjectId = 0, int topidId = 0, int timeUp = 0, int difficulty = 0, long status = 0);

    }
}

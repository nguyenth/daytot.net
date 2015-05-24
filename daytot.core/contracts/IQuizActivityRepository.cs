using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.projectors;

namespace daytot.core.contracts
{
    public interface IQuizActivityRepository: IBaseRepository<QuizActivity>
    {
        /// <summary>
        /// bắt đầu một bài thi
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="quizId"></param>
        /// <returns></returns>
        QuizActivity StartQuiz(int userId, int quizId);

        /// <summary>
        /// Tính toán kết quả một bài kiềm tra
        /// </summary>
        /// <param name="quizId">Mã bái kiềm tra</param>
        /// <param name="submition">Nội dung kết quả từ người dùng</param>
        /// <returns></returns>
        List<QuizSubmit> SubmitQuiz(int quizId, List<QuizSubmit> submition);

        /// <summary>
        /// Lấy danh sách bài thi của một user đã tham gia.
        /// </summary>
        /// <param name="userId">Mã tài khoản</param>
        /// <param name="classLevelId">Lọc theo lớp</param>
        /// <param name="subjectId">Lọc theo môn học</param>
        /// <param name="topicId">Lọc theo chuyên đề</param>
        /// <param name="timeUp">Lọc theo thời gian</param>
        /// <param name="total">Tồng số bài thi đã tham gia</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bài thi trên 1 trang</param>
        /// <returns></returns>
        List<QuizCompleteSmall> MyQuizListing(int userId, int classLevelId, int subjectId, int topicId, int timeUp, ref int total, int currentPage, int pageSize);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IQuizDetailRepository: IBaseRepository<QuizDetail>
    {
        /// <summary>
        /// Xóa tất cả QuizDetail theo một Quiz
        /// </summary>
        /// <param name="quizId">Quiz's Id</param>
        /// <returns></returns>
        void DeleteByQuiz(int quizId);

        /// <summary>
        /// Kiểm tra một QuizDetail đã tồn tại hay chưa
        /// </summary>
        /// <param name="quizDetail"></param>
        /// <returns></returns>
        bool IsExisted(QuizDetail quizDetail);

        /// <summary>
        /// Bổ sung 1 câu hỏi vào Quiz
        /// </summary>
        /// <param name="quizDetail"></param>
        bool AddQuestionToQuiz(QuizDetail quizDetail);

        /// <summary>
        /// Bổ sung 1 danh sách câu hỏi vào Quiz
        /// </summary>
        /// <param name="quizDetails"></param>
        int AddQuestionToQuiz(List<QuizDetail> quizDetails);

         /// <summary>
        /// Lấy danh sách câu hỏi của một bài thi.
        /// </summary>
        /// <param name="quizId">Mã bài thi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số Câu hỏi trên trang</param>
        /// <returns></returns>
        IQueryable<QuizDetail> GetQuestionByQuizId(int quizId, ref int total, int currentPage = 1, int pageSize = 25);
    }
}

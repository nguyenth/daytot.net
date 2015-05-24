using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.models;
using daytot.core.contracts;

namespace daytot.bll.repositories
{
    public class QuizDetailRepository: BaseRepository<QuizDetail>, IQuizDetailRepository
    {
        public QuizDetailRepository(DbContext dbContext) 
            : base(dbContext) { 

        }

        /// <summary>
        /// Xóa tất cả QuizDetail theo một Quiz
        /// </summary>
        /// <param name="quizId">Quiz's Id</param>
        /// <returns></returns>
        public void DeleteByQuiz(int quizId) {
            var q = _dbSet.Where(o => o.QuizId == quizId).ToList();
            Deletes(q);
        }

        /// <summary>
        /// Kiểm tra một QuizDetail đã tồn tại hay chưa
        /// </summary>
        /// <param name="quizDetail"></param>
        /// <returns></returns>
        public bool IsExisted(QuizDetail quizDetail) {
            return _dbSet.Any(o=>o.QuizId == quizDetail.QuizId && o.QuestionId == quizDetail.QuestionId);
        }

        /// <summary>
        /// Bổ sung 1 câu hỏi vào Quiz
        /// </summary>
        /// <param name="quizDetail"></param>
        public bool AddQuestionToQuiz(QuizDetail quizDetail)
        {
            if (!IsExisted(quizDetail))
            {
                Add(quizDetail);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Bổ sung 1 danh sách câu hỏi vào Quiz
        /// </summary>
        /// <param name="quizDetails"></param>
        public int AddQuestionToQuiz(List<QuizDetail> quizDetails)
        {
            int counter =0;
            foreach (var o in quizDetails)
            {
                if (AddQuestionToQuiz(o))
                    counter++;
            }
            return counter;
        }

        /// <summary>
        /// Lấy danh sách câu hỏi của một bài thi.
        /// </summary>
        /// <param name="quizId">Mã bài thi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số Câu hỏi trên trang</param>
        /// <returns></returns>
        public IQueryable<QuizDetail> GetQuestionByQuizId(int quizId, ref int total, int currentPage = 1, int pageSize = 25)
        {
            var query = _dbSet.AsNoTracking().Where(o=>o.QuizId == quizId).Include(q=>q.Question);
            total = query.Count();
            return query.OrderBy(o => o.QuestionId).Skip((currentPage - 1) * pageSize).Take(pageSize);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.core.models;
using daytot.core.contracts;
using daytot.core.projectors;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class QuizRepository: BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(DbContext dbContext) 
            : base(dbContext) { 
        }

        /// <summary>
        /// Lấy câu hỏi đầu đủ với thông tin câu hỏi
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        public Quiz GetFull(int quizId) {
            return _dbSet.AsNoTracking().Where(o => o.QuizId == quizId)
                        .Include(o => o.Questions.Select(q => q.Question))
                        .SingleOrDefault();
        }

        /// <summary>
        /// Lấy một bài tập của một Lecture
        /// </summary>
        /// <param name="lectureId">Mã bà giảng(Lecture's Id)</param>
        /// <returns></returns>
        public Quiz GetByLecture(int lectureId) {
            return _dbSet.AsNoTracking()
                        .Where(o => o.ReferId == lectureId && o.ReferTypeId == core.Consts.REFERTYPE_LECTURE)
                        .FirstOrDefault();
        }

        /// <summary>
        /// Thống kê số lượng đề thi theo lớp & môn học
        /// </summary>
        /// <returns></returns>
        public List<QuizSmallStat> GetSmallStatByClassAndSubject()
        {
            return ((DaytotDbContext)_dbContext).dspQuizs_GetSmallStatByClassAndSubject();
        }

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
        public List<QuizSmall> GetAll(ref int total, int currentPage = 1, int pageSize = 30,
            int classLevelId = 0, int subjectId = 0, int topidId = 0, int timeUp = 0, int difficulty = 0, long status = 0)
        {
            return ((DaytotDbContext)_dbContext).dspQuizs_GetAllExam(ref total, currentPage, pageSize, classLevelId, subjectId, topidId, timeUp, difficulty, status);
        }



    }
}

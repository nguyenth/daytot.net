using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.core.models;
using daytot.core.utils;
using daytot.core.contracts;
using daytot.core.projectors;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class QuizActivityRepository : BaseRepository<QuizActivity>, IQuizActivityRepository
    {
        public QuizActivityRepository(DbContext dbContext) :
            base(dbContext)
        {
        }

        /// <summary>
        /// bắt đầu một bài thi
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="quizId"></param>
        /// <returns></returns>
        public QuizActivity StartQuiz(int userId, int quizId){
            var last_activity = Get(userId, quizId);
            Quiz quiz = _uow.Quiz.GetFull(quizId);
            if (last_activity == null)
            {
                last_activity = new QuizActivity
                {
                    UserId = userId,
                    QuizId = quizId,
                    Attemps = 0,
                    Scores = 0,
                    Start = DateTime.Now,
                    TotalTime = quiz.TimeUp
                };

                Add(last_activity);
            }
            else
            {
                last_activity.Attemps += 1;
                last_activity.Start = DateTime.Now;
                last_activity.TotalTime = quiz.TimeUp;
                Update(last_activity);
            }

            _uow.Commit();

            return last_activity;
        }

        /// <summary>
        /// Tính toán kết quả một bài kiềm tra
        /// </summary>
        /// <param name="quizId">Mã bái kiềm tra</param>
        /// <param name="submition">Nội dung kết quả từ người dùng</param>
        /// <returns></returns>
        public List<QuizSubmit> SubmitQuiz(int quizId, List<QuizSubmit> submition) {
            return ((DaytotDbContext)_dbContext).dspQuizs_CalcScore(quizId, submition.ToXml());
        }

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
        public List<QuizCompleteSmall> MyQuizListing(int userId, int classLevelId, int subjectId, int topicId, int timeUp, ref int total, int currentPage, int pageSize)
        {
            return ((DaytotDbContext)_dbContext).dspQuizs_GetUserQuiz(ref total, currentPage, pageSize, userId, classLevelId, subjectId, topicId, timeUp);
        }

    }
}

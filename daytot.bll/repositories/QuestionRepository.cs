using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.core.models;
using daytot.core.contracts;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Thêm một cấu hỏi vào hệ thống
        /// </summary>
        /// <param name="question">Một Object Question chứ thông tin câu hỏi</param>
        /// <returns></returns>
        public Question AddOrEditQuestion(Question question)
        {
            if (question.QuestionId <= 0)
            {
                using (var trans = new System.Transactions.TransactionScope())
                {
                    question.Created = DateTime.Now;
                    question.Approved = false;

                    Add(question);
                    /*Commit Question*/
                    _uow.Commit();
                    ICollection<Answer> in_anses = question.Answers;
                    foreach (var ans in in_anses)
                    {
                        ans.QuestionId = question.QuestionId;
                        _uow.Answer.Add(ans);
                    }
                    /*Commit Answers*/
                    _uow.Commit();

                    question.Answers = in_anses;
                    Update(question);
                    /*Commit Question*/
                    _uow.Commit();

                    trans.Complete();
                }
            }
            else
            {
                var last_q = _uow.Question.Get(question.QuestionId);
                if (last_q != null)
                {
                    last_q.Description = question.Description;
                    var in_anses = question.Answers;
                    foreach (var ans in in_anses)
                    {
                        var last_ans = _uow.Answer.Get(ans.AnswerId);
                        if (last_ans != null)
                        {
                            last_ans.AnswerContent = ans.AnswerContent;
                            last_ans.IsCorrectly = ans.IsCorrectly;
                            _uow.Answer.Update(last_ans);
                        }
                    }
                    last_q.Answers = in_anses;
                    Update(last_q);
                    _uow.Commit();

                    question = last_q;
                }
                else
                    throw new InvalidOperationException(string.Format( "Không tồn tại câu hỏi có mã {0}", question.QuestionId));
            }
            return question;
        }

        /// <summary>
        /// Listing danh sách câu theo một dạng bái toán.
        /// </summary>
        /// <param name="topicId">Mã dạng bài toán</param>
        /// <param name="total">Tồng câu hỏi trả về</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang</param>
        /// <returns></returns>
        public IQueryable<Question> Listing(int topicId, ref int total, int currentPage, int pageSize)
        {

            currentPage = Math.Max(1, currentPage);
            pageSize = Math.Max(1, pageSize);
            var q = _dbSet.AsNoTracking().Where(o => o.TopicId == topicId);
            total = q.Count();
            return q.OrderByDescending(o => o.Created).Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Listing danh sách câu theo một môn học, lớp của một tài khoản.
        /// </summary>
        /// <param name="userId">Mã tài khoản</param>
        /// <param name="subjectId">Mã môn học</param>
        /// <param name="classLevelId">Cấp độ lớp</param>
        /// <param name="total">Tổng số câu hỏi thỏa điều kiện</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số câu hỏi hiển thị trên 1 trang</param>
        /// <returns></returns>
        public IQueryable<Question> Listing(int userId, int topicId, int subjectId, int classLevelId, ref int total, int currentPage, int pageSize)
        {

            currentPage = Math.Max(1, currentPage);
            pageSize = Math.Max(1, pageSize);
            var q = _dbSet.AsNoTracking().Where(o=>o.CreatedBy == userId);
            if (topicId > 0)
                q.Where(o => o.TopicId == topicId);
            else
                q.Join(_dbContext.Set<Topic>().Where(o => o.SubjectId == subjectId && o.TopicId == topicId), o => o.TopicId, i => i.TopicId, (o, i) => o);

            total = q.Count();
            return q.OrderByDescending(o => o.Created).Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Lấy danh sách câu hỏi theo điều kiện & phân trang.
        /// </summary>
        /// <param name="total">Tổng số câu hỏi thòa điều kiện. được trả về sau khi thực hiện hàm</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang cần lấy</param>
        /// <param name="classLevelId">Lớp</param>
        /// <param name="subjectId">Môn học</param>
        /// <param name="topicId">Chuyên đề</param>
        /// <returns></returns>
        public IQueryable<Question> GetAll(ref int total, int currentPage = 1, int pageSize = 30, int questionId = 0, int classLevelId = 0, int subjectId = 0, int topicId = 0)
        {
            currentPage = Math.Max(1, currentPage);
            pageSize = Math.Max(1, pageSize);

            return ((DaytotDbContext)_dbContext).dspQuestions_GetAll(ref total, currentPage, pageSize, questionId, classLevelId, subjectId, topicId).AsQueryable<Question>();
        }

    }
}

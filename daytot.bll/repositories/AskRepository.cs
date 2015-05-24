using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.contracts;
using daytot.core.models;
using daytot.core.projectors;

namespace daytot.bll.repositories
{
    public class AskRepository: BaseRepository<Ask>, IAskRepository
    {
        public AskRepository(DbContext dbContext)
            : base(dbContext)
        { 
        }

        /// <summary>
        /// Lấy danh sách câu hỏi của một User
        /// </summary>
        /// <param name="credential">Tài khoản người sử dụng</param>
        /// <param name="total">Tổng câu hỏi</param>
        /// <param name="curentPage">Trang cần lấy</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang</param>
        /// <returns></returns>
        public virtual IQueryable<Ask> GetAskeds(Credential credential, ref int total, int curentPage, int pageSize)
        {

            var query = GetAll();
            if (credential.IsTeacher)
            { 
            }
            else
                query = query.Where(o => o.UserId == credential.UserId);
            
            total = query.Count();

            return query
                    .Include(o=>o.Replies)
                    .OrderByDescending(o => o.Created)
                    .Skip((curentPage-1)*pageSize)
                    .Take(pageSize);
        }

        /// <summary>
        /// Đóng một câu hỏi
        /// </summary>
        /// <param name="askId"></param>
        public void Close(int askId)
        {
            Ask ask = Get(askId);
            if (ask != null)
            {
                ask.IsOpen = false;
                Update(ask);
            }
        }

        /// <summary>
        /// Lấy danh sách câu hỏi user đã trả lời
        /// </summary>
        /// <param name="UserId">User's Id</param>
        /// <param name="total">Tổng câu hỏi</param>
        /// <param name="curentPage">Trang cần lấy</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang</param>
        /// <returns></returns>
        public virtual IQueryable<Ask> GetAskWasReplied(int userId, ref int total, int curentPage, int pageSize)
        {
            var query = GetAll().GroupJoin(_uow.Reply.GetAll().Where(o => o.UserId == userId),
                                        a => a.AskId,
                                        r => r.AskId, (a, collectionR) => a);
            total = query.Count();

            return query.Include(a=>a.Asker)
                        .Include(a => a.Replies.Select(r=>r.Answer))
                        .OrderByDescending(o => o.Created)
                        .Skip((curentPage - 1) * pageSize)
                        .Take(pageSize);
        }

        /// <summary>
        /// Lấy danh sách câu hỏi theo một loại đối tượng liên quan
        /// </summary>
        /// <param name="referId">Mã đó tuông</param>
        /// <param name="referType">Loại đối tượng</param>
        /// <returns></returns>
        public IQueryable<Ask> GetByReferId(int referId, int referType, ref int total, int currenPage = 1, int pageSize = 10) {
            currenPage = Math.Max(1, currenPage);
            pageSize = Math.Min(10, pageSize);

            var q = _dbSet.AsNoTracking()
                            .Where(o => o.ReferId == referId && o.ReferType == referType);
            total = q.Count();
            return q.Include(a => a.Asker)
                     .Include(o => o.Replies.Select(a=>a.Answer))
                     .OrderByDescending(o=>o.Created).Skip((currenPage - 1) * pageSize).Take(pageSize);
        }

    }
}

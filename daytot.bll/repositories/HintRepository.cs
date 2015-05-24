using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class HintRepository : BaseRepository<Hint>, IHintRepository
    {
        public HintRepository(DbContext dbContext) : 
            base(dbContext) { 
        }

        /// <summary>
        /// Tạo mới hoặc cập nhật một gợi ý
        /// </summary>
        /// <param name="in_hint"></param>
        public void AddOrEditHint(Hint in_hint)
        {
            Question q = _uow.Question.Get(in_hint.QuestionId);
            if (q != null)
            {
                var hints = q.Hints;
                if (in_hint.HintId > 0)
                {
                    var existed = hints.Where(h=>h.HintId == in_hint.HintId).SingleOrDefault();
                    if (existed != null)
                    {
                        existed.HintContent = in_hint.HintContent;
                        existed.References = in_hint.References;
                        Update(existed);
                        in_hint = existed;
                    }
                    else
                        throw new Exception(string.Format("Không tồn tại gợi ý {0} cho câu hỏi {1}", in_hint.HintId, in_hint.QuestionId));
                }
                else
                {
                    in_hint.Step = hints.Count;
                    Add(in_hint);
                    _uow.Commit();

                    hints.Add(in_hint);
                    q.Hints = hints;

                }

                _uow.Commit();
            }
            else
                throw new Exception(string.Format("Không tồn tại câu hỏi {0} ", in_hint.QuestionId));
        }
    }
}

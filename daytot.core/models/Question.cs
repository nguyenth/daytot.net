using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using daytot.core.utils;

namespace daytot.core.models
{
    public class Question
    {
        /// <summary>
        /// Mã câu hỏi
        /// </summary>
        [Key]
        public int QuestionId { get; set; }

        /// <summary>
        /// Mã phân loại câu hỏi
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// Mô tả câu hỏi
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Loại câu hỏi
        /// 1. Trắc ngiệm
        /// </summary>
        public int Type { get; set; }
        
        /// <summary>
        /// Mức độ khó của câu hỏi
        /// </summary>
        public int Difficulty{ get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Trạng thái duyệt câu hỏi
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Tag cho câu hỏi
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Lưu danh sách câu hỏi dưới dạng Json
        /// </summary>
        public string AnswerObject { get; set; }

        /// <summary>
        /// Lưu danh sách gợi ý dưới dạng Json
        /// </summary>
        public string HintObject { get; set; }

        #region properties helper

        [NotMapped]
        public virtual ICollection<Answer> Answers
        {
            get {
                if (!string.IsNullOrEmpty(AnswerObject)) { 
                    return AnswerObject.FromJson<List<Answer>>();
                }
                return new List<Answer>();
            }
            set {
                if (value != null)
                    AnswerObject = value.ToJson();
                else
                    AnswerObject = null;
            }
        }

        ///// <summary>
        ///// Danh sách gợi ý
        ///// </summary>
       [NotMapped]
       public virtual ICollection<Hint> Hints
       {
           get
           {
               if (!string.IsNullOrEmpty(HintObject))
               {
                   return HintObject.FromJson<List<Hint>>();
               }
               return new List<Hint>();
           }
           set
           {
               if (value != null)
                   HintObject = value.ToJson();
               else
                   HintObject = null;
           }
       }

        #endregion

        #region methods helper
       public dynamic ToPublish() {
           return new { 
               QuestionId,
               Description,
               Type,
               Difficulty,
               Answers = Answers.OrderBy(o => Guid.NewGuid()).Select(o => o.ToPublish()).ToList()
           };
       }
        #endregion
    }
}

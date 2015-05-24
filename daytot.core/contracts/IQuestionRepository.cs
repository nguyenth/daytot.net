using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IQuestionRepository:IBaseRepository<Question>
    {
        
        /// <summary>
        /// Thêm một cấu hỏi vào hệ thống
        /// </summary>
        /// <param name="question">Một Object Question chứ thông tin câu hỏi</param>
        /// <returns></returns>
        Question AddOrEditQuestion(Question question);

        /// <summary>
        /// Listing danh sách câu theo một dạng bái toán.
        /// </summary>
        /// <param name="topicId">Mã dạng bài toán</param>
        /// <param name="total">Tồng câu hỏi trả về</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số câu hỏi trên 1 trang</param>
        /// <returns></returns>
        IQueryable<Question> Listing(int topicId, ref int total, int currentPage, int pageSize);

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
        IQueryable<Question> Listing(int userId, int topicId, int subjectId, int classLevelId, ref int total, int currentPage, int pageSize);

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
        IQueryable<Question> GetAll(ref int total, int currentPage = 1, int pageSize = 30, int questionId = 0, int classLevelId = 0, int subjectId = 0, int topicId = 0);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.projectors;

namespace daytot.core.contracts
{
    public interface ILearnActivityRepository:IBaseRepository<LearnActivity>
    {
        /// <summary>
        /// Bắt đầu thực hiện học một bài giảng
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <returns></returns>
        LearnActivity StartLearn(int enrollId, int lectureId);

        /// <summary>
        /// Lấy danh sách học tập của một ghi danh
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <returns></returns>
        List<LearnActivity> GetByEnrollId(int enrollId);

        /// <summary>
        /// Cập nhật thởi gian học một bài giảng
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <param name="spentTime">Thời gian đã học</param>
        /// <param name="duration">Thời lượng bài giảng</param>
        /// <returns></returns>
        bool LearnProgress(int enrollId, int lectureId, int spentTime, int duration);

        /// <summary>
        /// Xác nhận hoàn thành một khóa học
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <returns></returns>
        bool CompleteLearn(int enrollId, int lectureId);

        /// <summary>
        /// Lấy danh sách người dùng tham gia một khóa học.
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <returns></returns>
        List<StudentSmall> GetStudents(int courseId, int lectureId, bool connected, ref int total, int currentPage, int pageSize);

        /// <summary>
        /// Lấy danh sách học của một ghi danh
        /// </summary>
        /// <param name="enrollId">Mã ghi danh.</param>
        /// <returns></returns>
        List<LearnActivitySmall> GetLearnActivities(int enrollId);
    }
}

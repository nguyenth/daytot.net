using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IEnrollmentRepository: IBaseRepository<Enrollment>
    {
        /// <summary>
        /// Lấy thông tin ghi danh của một tài khoản theo 1 khóa học
        /// </summary>
        /// <param name="userId">Mã tài khoản</param>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        Enrollment GetByUserAndCourseId(int userId, int courseId);

        /// <summary>
        /// Thực hiện ghi danh.
        /// </summary>
        /// <param name="enroll"></param>
        /// <returns></returns>
        bool Enroll(Enrollment enroll);

        /// <summary>
        /// Hủy gi danh một khóa học
        /// </summary>
        /// <param name="enrollId"></param>
        /// <returns></returns>
        bool Unenroll(int enrollId);

        /// <summary>
        /// Kiểm tra tình trạng ghi danh khóa học
        /// </summary>
        /// <param name="userId">Mã tài khoản</param>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        int CheckEnroll(int userId, int courseId);

        /// <summary>
        /// Kiểm tra một khóa học
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        int CheckLectureInEnroll(int userId, int lectureId);

         /// <summary>
        /// Lấy danh sách đang ghi danh của một khóa học
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        IQueryable<Enrollment> Gets(int courseId);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface ILectureRepository: IBaseRepository<Lecture>
    {
        /// <summary>
        /// Kiểm tra một Lecture có thuộc tài khoản này hay không
        /// </summary>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <param name="userId">Mã người dùng</param>
        /// <returns></returns>
        bool IsOwner(int lectureId, int userId);

        /// <summary>
        /// Lấy thông tin khóa học của một bài giảng
        /// </summary>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        Course GetCourse(int lectureId);

        /// <summary>
        /// Lấy bài giảng theo phiên bản
        /// </summary>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <param name="isDraft">Phiên bản:
        /// true: Lấy phiên bản được người dùng cập nhật mới nhất.
        /// false: lấy phiên bản được duyệt gần nhất
        /// </param>
        /// <returns></returns>
        Lecture GetVersion(int lectureId, bool isDraft);

        /// <summary>
        /// Chỉnh sửa một khóa học.
        /// </summary>
        /// <param name="in_lecture"></param>
        void Edit(Lecture in_lecture);

        /// <summary>
        /// Danh sách bài giảng của một khóa học
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IQueryable<Lecture> GetByCourseId(int courseId);
    }
}

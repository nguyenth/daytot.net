using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.projectors;

namespace daytot.core.contracts
{
    public interface ICourseRepository: IBaseRepository<Course>
    {
        /// <summary>
        /// Kiểm tra quyền làm chủ của một tài khoản đối với một khóa học
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="userId">Mã tài khoản</param>
        /// <returns></returns>
        bool IsOwner(int courseId, int userId);

        /// <summary>
        /// Lấy danh sách khóa học của một tài khoản đã tạo
        /// </summary>
        /// <param name="userId">Mã tài hoản</param>
        /// <returns></returns>
        List<Course> GetOwnerCourse(int userId);

         /// <summary>
        /// Lấy nội dung khóa học theo phiên bản.
        /// </summary>
        /// <param name="courseId">Khóa chính khóa học</param>
        /// <param name="isDraft">Phiên bản cần lấy
        /// True: Ưu tiên phiên bản chưa duyệt.
        /// False: Ưu tiên phiên bản được duyệt.
        /// </param>
        /// <returns></returns>
        Course GetVersion(int courseId, bool isDraft = false);

        /// <summary>
        /// Lấy một khóa học và thông tin giáo viên đi kèm
        /// </summary>
        /// <param name="id">Mã khóa học</param>
        /// <param name="isFull">True: lấy th6ong tin gáo viên kèm, False: chỉ lấy thông tin khóa học </param>
        /// <returns></returns>
        Course GetVersion(int id, bool isFull, bool isDraft);

        /// <summary>
        /// Cập nhật một khoá học
        /// </summary>
        /// <param name="in_course"></param>
        void Edit(Course in_course);

        /// <summary>
        /// Lisitng danh sách khóa học
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="classLevelId">Lớp học</param>
        /// <param name="subjectId">Mã môn học</param>
        /// <param name="type">Loại khóa học</param>
        /// <param name="status">Tình trạng</param>
        /// <param name="total">Tồng số khóa học</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số khóa học trên 1 trang</param>
        /// <returns></returns>
        IQueryable<CourseSmall> GetAll(int courseId, int classLevelId, int subjectId, int type, long status, bool isPublicOnly, ref int total, int currentPage, int pageSize);

        /// <summary>
        /// Lấy danh sách khóa học tiêu biểu
        /// </summary>
        /// <returns></returns>
        List<CourseSmall> GetTop();

         /// <summary>
        /// Lấy danh sách khóa học của một tài khoản đã tham gia
        /// </summary>
        /// <param name="userId">Mã tài hoản</param>
        /// <param name="total">Tổng kháo học đã ghi danh</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số khóa học trên 1 trang</param>
        /// <returns></returns>
        List<CourseSmall> GetCourseEnrolled(int userId, ref int total, int currentPage, int pageSize);
    }
}

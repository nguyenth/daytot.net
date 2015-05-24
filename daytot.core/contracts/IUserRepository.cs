using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;
using daytot.core.projectors;

namespace daytot.core.contracts
{
    public interface IUserRepository: IBaseRepository<User>
    {
        /// <summary>
        /// Lấy một User từ DB theo tên đăng nhập
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <returns>Nếu không tồn tại User nào thì trả về null</returns>
        User GetByUsername(string username);

        ///// <summary>
        ///// Lấy top teacher tham gia vào daytot.net
        ///// </summary>
        ///// <param name="total">Tổng số giáo viên của daytot.net trả về sau khi thực thi</param>
        ///// <param name="top">Số lượng top cần lấy</param>
        ///// <returns></returns>
        //IQueryable<User> GetTopTeacher(ref int total, int top);

        ///// <summary>
        ///// Liệt kê danh sách tài khoản giáo viên
        ///// </summary>
        ///// <param name="total">Tồng số giáo viên trong hệ thống</param>
        ///// <param name="currentPage">Trang hiển thị hiện tại</param>
        ///// <param name="pageSize">Số giao viên hiển thị trên một trang</param>
        ///// <param name="subjecId">Môn học</param>
        ///// <param name="approved">Tình trạng duyệt</param>
        ///// <param name="activated">Tình trạng kích hoạt</param>
        ///// <returns></returns>
        //IQueryable<User> TeacherListing(ref int total, int currentPage, int pageSize, int subjecId, bool? approved, bool? activated);

        /// <summary>
        /// Liệt kê danh sách tài khoản theo phân trang
        /// </summary>
        /// <param name="type">Loại tài khoản</param>
        /// <param name="subjectId">Môn học</param>
        /// <param name="status">Tình trạng tài khoản</param>
        /// <param name="email">Email</param>
        /// <param name="total">Tổng số tài khoản trong hệ thống</param>
        /// <param name="currentPage">Trang hệ thống</param>
        /// <param name="pageSize">Số tài khoản trên 1 trang</param>
        /// <returns></returns>
        IQueryable<UserSmall> GetAll(int type, int subjectId, long status, string email, ref int total, int currentPage, int pageSize);
        
        /// <summary>
        /// Ghi nhận một tài khoản Online
        /// </summary>
        /// <param name="connection"></param>
        void Online(OnlineConnection connection);

        /// <summary>
        /// Ghi nhận một tài khoản Offline
        /// </summary>
        /// <param name="connection"></param>
        void Offline(OnlineConnection connection);
    }
}
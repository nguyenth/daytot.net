using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.contracts;
using daytot.core.helpers;
using daytot.core.models;
using daytot.core.projectors;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Lấy một User từ DB theo tên đăng nhập
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <returns>Nếu không tồn tại User nào thì trả về null</returns>
        public User GetByUsername(string username)
        {
            try
            {
                return _dbSet.AsNoTracking()
                             .Include(u=>u.Connections)
                             .SingleOrDefault(u => u.Email == username);
            }
            catch (Exception ex)
            {
                Logger.Error("UserRepository.GetByUsername", ex);
            }
            return null;
        }

        /// <summary>
        /// Lấy top teacher tham gia vào daytot.net
        /// </summary>
        /// <param name="total">Tổng số giáo viên của daytot.net trả về sau khi thực thi</param>
        /// <param name="top">Số lượng top cần lấy</param>
        /// <returns></returns>
        public IQueryable<User> GetTopTeacher(ref int total, int top)
        {
            total = _dbSet.Where(u => (u.Type & core.Consts.ACCOUN_TYPE_TEACHER) == core.Consts.ACCOUN_TYPE_TEACHER
                                        && u.Approved).Count();
            return _dbSet.Where(u => (u.Type & core.Consts.ACCOUN_TYPE_TEACHER) == core.Consts.ACCOUN_TYPE_TEACHER
                                        && u.Approved)
                            .OrderByDescending(u => u.UserId).Take(top);
        }

        /// <summary>
        /// Liệt kê danh sách tài khoản giáo viên
        /// </summary>
        /// <param name="total">Tồng số giáo viên trong hệ thống</param>
        /// <param name="currentPage">Trang hiển thị hiện tại</param>
        /// <param name="pageSize">Số giao viên hiển thị trên một trang</param>
        /// <param name="subjecId">Môn học</param>
        /// <param name="approved">Tình trạng duyệt</param>
        /// <param name="activated">Tình trạng kích hoạt</param>
        /// <returns></returns>
        public IQueryable<User> TeacherListing(ref int total, int currentPage, int pageSize, int subjecId, bool? approved, bool? activated)
        {
            var query = _dbSet.Where(u => (u.Type & core.Consts.ACCOUN_TYPE_TEACHER) == core.Consts.ACCOUN_TYPE_TEACHER);
            if (subjecId > 0)
                query = query.Where(u => u.SubjectId == subjecId);
            if (approved.HasValue)
                query = query.Where(u => u.Approved == approved);
            if (activated.HasValue && activated.Value)
                query = query.Where(u => (u.Status & core.Consts.ACCOUN_STATUS_UNACTIVE) != core.Consts.ACCOUN_STATUS_UNACTIVE);
            if (activated.HasValue && !activated.Value)
                query = query.Where(u => (u.Status & core.Consts.ACCOUN_STATUS_UNACTIVE) == core.Consts.ACCOUN_STATUS_UNACTIVE);

            total = query.Count();
            return query.OrderByDescending(o => o.UserId).Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Ghi nhận một tài khoản Online
        /// </summary>
        /// <param name="connection"></param>
        public void Online(OnlineConnection connection) {
            try {
                OnlineConnection any = _uow.OnlineConnection.Get(connection.ConnectionID);
                if (any != null)
                {
                    any.Connected = true;
                    any.UserAgent = connection.UserAgent;
                    any.ConnectionID = connection.ConnectionID;

                    _uow.OnlineConnection.Update(any);
                }
                else {
                    _uow.OnlineConnection.Add(connection);
                }
            }
            catch (Exception ex) {
                Logger.Error("UserRepository.Online", ex);
            }
        }

        /// <summary>
        /// Ghi nhận một tài khoản Online
        /// </summary>
        /// <param name="connection"></param>
        public void Offline(OnlineConnection connection)
        {
            try
            {
                OnlineConnection any = _uow.OnlineConnection.Get(connection.ConnectionID);
                if (any != null)
                {
                    any.Connected = false;
                    any.UserAgent = connection.UserAgent;

                    _uow.OnlineConnection.Delete(any);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("UserRepository.Offline", ex);
            }
        }

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
        public IQueryable<UserSmall> GetAll(int type, int subjectId, long status, string email, ref int total, int currentPage, int pageSize)
        {
            return ((DaytotDbContext)_dbContext).dspUsers_Listing(ref total, currentPage, pageSize, type, subjectId, status, email).AsQueryable<UserSmall>();

        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.models;
using daytot.core.contracts;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Kiểm tra tình trạng ghi danh khóa học
        /// </summary>
        /// <param name="userId">Mã tài khoản</param>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        public int CheckEnroll(int userId, int courseId) {
            var enrollment = _dbSet.AsNoTracking().Where(o => o.UserId == userId && o.CourseId == courseId && !o.IsDeleted).SingleOrDefault();
            if (enrollment != null)
                return enrollment.EnrollId;
            return 0;
        }

        /// <summary>
        /// Kiểm tra một khóa học
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        public int CheckLectureInEnroll(int userId, int lectureId) {
            return ((DaytotDbContext)_dbContext).dspEnrollments_CheckLectureInEnroll(userId, lectureId);
        }

        /// <summary>
        /// Lấy thông tin ghi danh của một tài khoản theo 1 khóa học
        /// </summary>
        /// <param name="userId">Mã tài khoản</param>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        public Enrollment GetByUserAndCourseId(int userId, int courseId) {
            return _dbSet.AsEnumerable().Where(o=>o.UserId == userId && o.CourseId == courseId).SingleOrDefault();
        }

        /// <summary>
        /// Thực hiện ghi danh.
        /// </summary>
        /// <param name="enroll"></param>
        /// <returns></returns>
        public bool Enroll(Enrollment enroll) {
            if (enroll.EnrollId == 0)
            {
                enroll.EnrollDate = DateTime.Now;
                Add(enroll);
            }
            else if (enroll.IsDeleted)
            {
                enroll.IsDeleted = false;
                enroll.Deleted = null;
                enroll.EnrollDate = DateTime.Now;

                Update(enroll);
            }
            else
                return false;
            _uow.Commit();

            return true;
        }


        /// <summary>
        /// Hủy gi danh một khóa học
        /// </summary>
        /// <param name="enrollId"></param>
        /// <returns></returns>
        public bool Unenroll(int enrollId)
        {
            var enroll = Get(enrollId);
            if (enroll != null)
            {
                enroll.IsDeleted = true;
                enroll.Deleted = DateTime.Now;

                Update(enroll);
                _uow.Commit();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lấy danh sách đang ghi danh của một khóa học
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        public IQueryable<Enrollment> Gets(int courseId) {
            return _dbSet.AsNoTracking().Where(o=>o.CourseId == courseId && !o.IsDeleted && !o.IsCompleted);
        }

    }
}

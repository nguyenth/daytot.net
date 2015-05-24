using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using daytot.core.contracts;
using daytot.core.models;
using daytot.core.caching;
using daytot.core.projectors;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Kiểm tra quyền làm chủ của một tài khoản đối với một khóa học
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="userId">Mã tài khoản</param>
        /// <returns></returns>
        public virtual bool IsOwner(int courseId, int userId)
        {
            return _dbSet.Any(o => (o.CourseId == courseId && o.UserId == userId));
        }

        /// <summary>
        /// Lấy nội dung khóa học theo phiên bản.
        /// </summary>
        /// <param name="courseId">Khóa chính khóa học</param>
        /// <param name="isDraft">Phiên bản cần lấy
        /// True: Ưu tiên phiên bản chưa duyệt.
        /// False: Ưu tiên phiên bản được duyệt.
        /// </param>
        /// <returns></returns>
        public Course GetVersion(int courseId, bool isDraft = false)
        {
            var course = Get(courseId);
            if (isDraft)
                course.ApplyDraft();
            return course;
        }

        /// <summary>
        /// Lấy danh sách khóa học của một tài khoản đã tạo
        /// </summary>
        /// <param name="userId">Mã tài hoản</param>
        /// <returns></returns>
        public virtual List<Course> GetOwnerCourse(int userId)
        {
            return _dbSet.AsNoTracking().Where(o => o.UserId == userId && (o.Status & core.Consts.STATUS_DELETED) == 0 ).ToList();
        }

        /// <summary>
        /// Lấy một khóa học và thông tin giáo viên đi kèm
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="isFull">True: lấy thông tin gáo viên kèm, False: chỉ lấy thông tin khóa học </param>
        /// <param name="isDraft">Phiên bản cần lấy
        /// True: Ưu tiên phiên bản chưa duyệt.
        /// False: Ưu tiên phiên bản được duyệt.
        /// </param>
        /// <returns></returns>
        public Course GetVersion(int courseId, bool isFull, bool isDraft)
        {
            if (!isFull)
                return GetVersion(courseId, isDraft);

            var course = _dbSet.AsNoTracking()
                         .Where(o => o.CourseId == courseId)
                         .Include(o => o.Teachers.Select(u => u.Teacher))
                         .SingleOrDefault();

            if (isDraft)
                course.ApplyDraft();

            return course;
        }

        /// <summary>
        /// Cập nhật một khoá học
        /// </summary>
        /// <param name="in_course"></param>
        public void Edit(Course in_course)
        {

            Course last_course = GetVersion(in_course.CourseId, true);
            bool isApproved = last_course.IsApproved;
            if (isApproved)
            {
                last_course.DraftObject = in_course.ToDraft();

            }
            else
            {
                last_course.CourseName = in_course.CourseName;
                last_course.IntroImage = in_course.IntroImage;
                last_course.IntroVideo = in_course.IntroVideo;
                last_course.Intro = in_course.Intro;
                last_course.Requirement = in_course.Requirement;
                last_course.HowToLearn = in_course.HowToLearn;
                last_course.Syllabus = in_course.Syllabus;
                last_course.Tags = in_course.Tags;
                last_course.Updated = in_course.Updated;
                last_course.UpdatedBy = in_course.UpdatedBy;
                last_course.LastIP = in_course.LastIP;
            }
            last_course.IsInit = false;
            last_course.IsDraft = true;

            Update(last_course);
            last_course.ApplyDraft();
            in_course =  last_course;
        }

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
        public IQueryable<CourseSmall> GetAll(int courseId, int classLevelId, int subjectId, int type, long status, bool isPublicOnly, ref int total, int currentPage, int pageSize)
        {
            return ((DaytotDbContext)_dbContext).dspCourses_Listing(ref total, currentPage, pageSize, courseId, classLevelId, subjectId, type, status, isPublicOnly, string.Empty).AsQueryable<CourseSmall>();

        }

        /// <summary>
        /// Lấy danh sách khóa học tiêu biểu
        /// </summary>
        /// <returns></returns>
        public List<CourseSmall> GetTop()
        {
            return ((DaytotDbContext)_dbContext).dspCourses_Top();

        }

        /// <summary>
        /// Lấy danh sách khóa học của một tài khoản đã tham gia
        /// </summary>
        /// <param name="userId">Mã tài hoản</param>
        /// <param name="total">Tổng kháo học đã ghi danh</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số khóa học trên 1 trang</param>
        /// <returns></returns>
        public List<CourseSmall> GetCourseEnrolled(int userId, ref int total, int currentPage, int pageSize)
        {
            return ((DaytotDbContext)_dbContext).dspCourses_EnrolledListing(ref total, currentPage, pageSize, userId);
        }

        /// <summary>
        /// Chạy trigger cập nhật lại khóa học 
        /// </summary>
        /// <param name="entity"></param>
        public override void PreUpdate(Course entity)
        {
            entity.IsPublic = entity.IsPublished && entity.IsApproved;
            entity.Updated = DateTime.Now;
        }

        #region caching
        public override void UpdateCache(Course entity)
        {

        }
        #endregion

    }
}

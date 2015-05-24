using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
        public SectionRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Lấy danh sách Section của một khóa học bào gồm các bào giảng đi kèm
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        public virtual List<Section> GetCurriculum(int courseId)
        {
            return _dbSet.AsNoTracking()
                        .Where(s => s.CourseId == courseId)
                        .Include(s => s.Lectures)
                        .ToList();
        }

        /// <summary>
        /// Lấy danh sách Section của một khóa học bao gồm các bài giảng đi kèm đã được công khai
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        public virtual List<Section> GetPublishCurriculum(int courseId)
        {
            return _dbSet.AsNoTracking()
                        .Where(s => s.CourseId == courseId /*&& (s.Status & core.Consts.STATUS_APPROVED) == core.Consts.STATUS_APPROVED */)
                        .Include(s => s.Lectures)
                        .ToList();
        }

        /// <summary>
        /// Kiểm tra một Section có thuộc tài khoản này hay không
        /// </summary>
        /// <param name="sectionId">Mã bài giảng</param>
        /// <param name="userId">Mã người dùng</param>
        /// <returns></returns>
        public bool IsOwner(int sectionId, int userId)
        {
            return _dbSet.AsNoTracking()
                .Where(o => o.SectionId == sectionId)
                .Join(_dbContext.Set<Course>(), o => o.CourseId, o => o.CourseId, (l, c) => new { UserId = c.UserId })
                .Any(o => o.UserId == userId);
        }

        /// <summary>
        /// Lấy danh sách bài giảng của một Section
        /// </summary>
        /// <param name="sectionId">Mã Section</param>
        /// <returns></returns>
        public ICollection<Lecture> GetLectures(int sectionId) {
            return _dbSet.AsNoTracking()
                        .Where(o => o.SectionId == sectionId)
                        .Include(o => o.Lectures)
                        .Select(o => o.Lectures)
                        .SingleOrDefault();
        }

    }
}

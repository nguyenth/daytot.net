using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

using daytot.core.contracts;
using daytot.core.models;
using daytot.core.projectors;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class LearnActivityRepository : BaseRepository<LearnActivity>, ILearnActivityRepository
    {
        public LearnActivityRepository(DbContext dbContext) :
            base(dbContext)
        {
        }

        /// <summary>
        /// Bắt đầu thực hiện học một bài giảng
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <returns></returns>
        public LearnActivity StartLearn(int enrollId, int lectureId) {
            var last_activity = _uow.LearnActivity.Get(enrollId, lectureId);
            if (last_activity == null)
            {
                last_activity = new LearnActivity
                {
                    EnrollId = enrollId,
                    LectureId = lectureId,
                    Attemps = 0,
                    Completed = 0,
                    LastView = DateTime.Now,
                    SpentTime = 0
                };
                Add(last_activity);
            }
            else {
                last_activity.Attemps += 1;
                last_activity.LastView = DateTime.Now;
                Update(last_activity);
            }
            _uow.Commit();
            return last_activity;
        }

        /// <summary>
        /// Lấy danh sách học tập của một ghi danh
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <returns></returns>
        public List<LearnActivity> GetByEnrollId(int enrollId) {
            return _dbSet.AsNoTracking().Where(o=>o.EnrollId == enrollId).ToList();
        }

        /// <summary>
        /// Cập nhật thởi gian học một bài giảng
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <param name="spentTime">Thời gian đã học</param>
        /// <param name="duration">Thời lượng bài giảng</param>
        /// <returns></returns>
        public bool LearnProgress(int enrollId, int lectureId, int spentTime, int duration)
        {
            var last_activity = _uow.LearnActivity.Get(enrollId, lectureId);
            int percent = (spentTime * 100) / duration;
            if (last_activity != null && last_activity.Completed < percent)
            {
                last_activity.SpentTime = spentTime;
                last_activity.Completed = percent;
                Update(last_activity);
                _uow.Commit();
            }
            else
                return false;
            return true;
        }


        /// <summary>
        /// Xác nhận hoàn thành một khóa học
        /// </summary>
        /// <param name="enrollId">Mã ghi danh</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <returns></returns>
        public bool CompleteLearn(int enrollId, int lectureId)
        {
            var last_activity = _uow.LearnActivity.Get(enrollId, lectureId);
            if (last_activity != null && last_activity.Completed < 100 && last_activity.Completed >= 80)
            {
                last_activity.Completed = 100;

                var enroll = _uow.Enrollment.Get(enrollId);
                enroll.IsCompleted = true;

                Update(last_activity);
                _uow.Enrollment.Update(enroll);
                _uow.Commit();
            }
            else
                return false;
            return true;
        }

        /// <summary>
        /// Lấy danh sách người dùng tham gia một khóa học.
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <returns></returns>
        public List<StudentSmall> GetStudents(int courseId, int lectureId, bool connected, ref int total, int currentPage, int pageSize)
        {
            return ((DaytotDbContext)_dbContext).dspLearnActivities_GetStudents(courseId, lectureId, connected, ref total, currentPage, pageSize);
        }
        
        /// <summary>
        /// Lấy danh sách học của một ghi danh
        /// </summary>
        /// <param name="enrollId">Mã ghi danh.</param>
        /// <returns></returns>
        public List<LearnActivitySmall> GetLearnActivities(int enrollId){
            return ((DaytotDbContext)_dbContext).dspLearnActivities_GetLearnActivitiesByEnrollId(enrollId);
        }

    }
}

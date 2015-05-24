using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll.repositories
{
    public class LectureRepository : BaseRepository<Lecture>, ILectureRepository
    {
        public LectureRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Kiểm tra một Lecture có thuộc tài khoản này hay không
        /// </summary>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <param name="userId">Mã người dùng</param>
        /// <returns></returns>
        public bool IsOwner(int lectureId, int userId) {
            try {
                Lecture lecture = Get(lectureId);
                if (lecture != null)
                    return _uow.Section.IsOwner(lecture.SectionId, userId);
            }
            catch (Exception ex) {
                core.helpers.Logger.Error(string.Format("LectureRepository.IsOwner(lectureId={0},userId={1})", lectureId, userId), ex);
            }
            return false;
        }

        /// <summary>
        /// Lấy thông tin khóa học của một bài giảng
        /// </summary>
        /// <param name="lectureId"></param>
        /// <returns></returns>
        public Course GetCourse(int lectureId) {
            var lecture = Get(lectureId);
            if (lecture != null)
            {
                Section section = _uow.Section.Get(lecture.SectionId);
                Course course = _uow.Course.Get(section.CourseId);
                return course;
            }
            return null;
        }

        /// <summary>
        /// Lấy bài giảng theo phiên bản
        /// </summary>
        /// <param name="lectureId">Mã bài giảng</param>
        /// <param name="isDraft">Phiên bản:
        /// true: Lấy phiên bản được người dùng cập nhật mới nhất.
        /// false: lấy phiên bản được duyệt gần nhất
        /// </param>
        /// <returns></returns>
        public Lecture GetVersion(int lectureId, bool isDraft) {
            var lecture = Get(lectureId);
            if (isDraft)
                lecture.ApplyDraft();
            return lecture;
        }

        /// <summary>
        /// Chỉnh sửa một khóa học.
        /// </summary>
        /// <param name="in_lecture"></param>
        public void Edit(Lecture in_lecture) {

            var last_lecture = GetVersion(in_lecture.LectureId, true);
            bool isApproved = last_lecture.IsApproved;
            if (isApproved)
            {
                last_lecture.SectionId = in_lecture.SectionId;
                last_lecture.DraftObject = in_lecture.ToDraft();
            }
            else {
                last_lecture.SectionId = in_lecture.SectionId;

                last_lecture.LectureTitle = in_lecture.LectureTitle;
                last_lecture.MaterialObject = in_lecture.MaterialObject;
                last_lecture.VideoJson = in_lecture.VideoJson;
                last_lecture.KnowledgeSummary = in_lecture.KnowledgeSummary;
                last_lecture.HomeworkObject = in_lecture.HomeworkObject;

            }

            last_lecture.IsDraft = true;
            last_lecture.IsWaitForApprove = false;

            Update(last_lecture);

        }

        /// <summary>
        /// Danh sách bài giảng của một khóa học
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public IQueryable<Lecture> GetByCourseId(int courseId) {
            return _dbSet.AsNoTracking().Join(_dbContext.Set<Section>().Where(s => s.CourseId == courseId), o => o.SectionId, i => i.SectionId, (o, i) => o);
        }

    }
}

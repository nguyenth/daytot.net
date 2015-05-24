using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface ISectionRepository: IBaseRepository<Section>
    {
        /// <summary>
        /// Lấy danh sách Section của một khóa học bào gồm các bào giảng đi kèm
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        List<Section> GetCurriculum(int courseId);

        /// <summary>
        /// Lấy danh sách Section của một khóa học bao gồm các bài giảng đi kèm đã được công khai
        /// </summary>
        /// <param name="courseId">Mã khóa học</param>
        /// <returns></returns>
        List<Section> GetPublishCurriculum(int courseId);

         /// <summary>
        /// Kiểm tra một Section có thuộc tài khoản này hay không
        /// </summary>
        /// <param name="sectionId">Mã Section</param>
        /// <param name="userId">Mã người dùng</param>
        /// <returns></returns>
        bool IsOwner(int sectionId, int userId);

        /// <summary>
        /// Lấy danh sách bài giảng của một Section
        /// </summary>
        /// <param name="sectionId">Mã Section</param>
        /// <returns></returns>
        ICollection<Lecture> GetLectures(int sectionId);
    }
}

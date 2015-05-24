using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using daytot.dal;
using daytot.bll.helpers;
using daytot.core.contracts;
using daytot.core.models;

namespace daytot.bll
{
    public class Uow : IUow, IDisposable
    {
        private DaytotDbContext _dbContext;
        protected IRepositoryProvider _repositoryProvider;

        public Uow(IRepositoryProvider repositoryProvider)
        {
            _dbContext = new DaytotDbContext();

            _dbContext.Configuration.ProxyCreationEnabled = false;
            _dbContext.Configuration.LazyLoadingEnabled = false;
            _dbContext.Configuration.ValidateOnSaveEnabled = false;
            _dbContext.Configuration.ProxyCreationEnabled = false;

            repositoryProvider.DbContext = _dbContext;
            _repositoryProvider = repositoryProvider;
        }

        /// <summary>
        /// Commit các dữ liệu thay đổi  xuống Database
        /// </summary>
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        /*Daytot repository*/
        public IUserRepository User { get { return GetRepo<IUserRepository>(); } }

        public IAskRepository Ask { get { return GetRepo<IAskRepository>(); } }
        public IReplyRepository Reply { get { return GetRepo<IReplyRepository>(); } }

        public IBaseRepository<OnlineConnection> OnlineConnection { get { return GetStandardRepo<OnlineConnection>(); } }
        public IBaseRepository<ClassLevel> ClassLevels { get { return GetStandardRepo<ClassLevel>(); } }
        public IBaseRepository<Subject> Subjects { get { return GetStandardRepo<Subject>(); } }
        public IBaseRepository<DifficultLevel> DifficultLevels { get { return GetStandardRepo<DifficultLevel>(); } }

        public IMediaRepository Media { get { return GetRepo<IMediaRepository>(); } }
        public IMediaServerRepository MediaServer { get { return GetRepo<IMediaServerRepository>(); } }

        public ICourseRepository Course { get { return GetRepo<ICourseRepository>(); } }
        public ISectionRepository Section { get { return GetRepo<ISectionRepository>(); } }
        public ILectureRepository Lecture { get { return GetRepo<ILectureRepository>(); } }
        public ITopicRepository Topic { get { return GetRepo<ITopicRepository>(); } }
        public IQuestionRepository Question { get { return GetRepo<IQuestionRepository>(); } }
        public IHintRepository Hint { get { return GetRepo<IHintRepository>(); } }
        public IAnswerRepository Answer { get { return GetRepo<IAnswerRepository>(); } }

        public IQuizRepository Quiz { get { return GetRepo<IQuizRepository>(); } }
        public IQuizDetailRepository QuizDetail { get { return GetRepo<IQuizDetailRepository>(); } }

        public IFavoriteRepository Favorite { get { return GetRepo<IFavoriteRepository>(); } }

        public IEnrollmentRepository Enrollment { get { return GetRepo<IEnrollmentRepository>(); } }

        public ILearnActivityRepository LearnActivity { get { return GetRepo<ILearnActivityRepository>(); } }

        public IQuizActivityRepository QuizActivity { get { return GetRepo<IQuizActivityRepository>(); } }

        /// <summary>
        /// Lấy một đối tượng Repository theo kiểu T xác định
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IBaseRepository<T> GetStandardRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepositoryForEntityType<T>();
        }

        /// <summary>
        /// Lấy một Repository cơ bản theo kiểu T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepository<T>();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }

        #endregion
    }
}

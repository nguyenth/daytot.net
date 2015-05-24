using System;
using System.Collections.Generic;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IUow
    {

        /// <summary>
        /// Commit các thay đổi xuống Database
        /// </summary>
        void Commit();

        IUserRepository User { get;  }
        IAskRepository Ask { get; }
        IReplyRepository Reply { get; }

        IBaseRepository<OnlineConnection> OnlineConnection { get; }
        IBaseRepository<ClassLevel> ClassLevels { get;  }
        IBaseRepository<Subject> Subjects { get; }
        IBaseRepository<DifficultLevel> DifficultLevels { get; }

        IMediaRepository Media { get; }
        IMediaServerRepository MediaServer { get; }
        ICourseRepository Course { get; }
        ISectionRepository Section { get;  }
        ILectureRepository Lecture { get; }
        ITopicRepository Topic { get; }
        IQuestionRepository Question { get; }
        IHintRepository Hint { get; }
        IAnswerRepository Answer { get; }
        IQuizRepository Quiz { get ; }
        IQuizDetailRepository QuizDetail { get; }

        IFavoriteRepository Favorite { get; }
        IEnrollmentRepository Enrollment { get; }
        ILearnActivityRepository LearnActivity { get; }
        IQuizActivityRepository QuizActivity { get; }
    }
}

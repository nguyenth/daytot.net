namespace daytot.dal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using daytot.core.models;
    using daytot.core.utils;

    internal sealed class Configuration : DbMigrationsConfiguration<daytot.dal.DaytotDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(daytot.dal.DaytotDbContext context)
        {
            context.ClassLevels.AddOrUpdate(
                                         o => o.ClassName,
                                         new ClassLevel { ClassName = "LTĐH", Level =  14},
                                         new ClassLevel { ClassName = "Lớp 12", Level =  12 },
                                         new ClassLevel { ClassName = "Lớp 11" , Level =  11},
                                         new ClassLevel { ClassName = "Lớp 10", Level =  10 }
                                       );

            context.Subjects.AddOrUpdate(
                                            s => s.SubjectName,
                                            new Subject { SubjectName = "Toán" },
                                            new Subject { SubjectName = "Vật lý" },
                                            new Subject { SubjectName = "Hóa" }
                                        );

            context.DifficultLevels.AddOrUpdate(
                                        diff => diff.Id,
                                        new DifficultLevel { Id = 1, Title = "Dễ", Level = 1 },
                                        new DifficultLevel { Id = 2, Title = "Trung bình", Level = 2 },
                                        new DifficultLevel { Id = 3, Title = "Khó", Level = 3 }
                                    );
            context.Questions.ToList().ForEach(delegate(Question q) {
                q.AnswerObject = context.Answers.Where(a => a.QuestionId == q.QuestionId).ToList().ToJson();
            });
            context.SaveChanges();
        }
    }
}

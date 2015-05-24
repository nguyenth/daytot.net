using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;

using daytot.core.models;

namespace daytot.dal
{
    public class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
            this.SetDatabaseInitializer(new DropCreateDatabaseAlways<DaytotDbContext>());
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }

    [DbConfigurationType(typeof(DbContextConfiguration))]
    public partial class DaytotDbContext: DbContext
    {
        public DaytotDbContext() :
            base("daytot.db")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>().HasKey(t => new { t.UserId, t.CourseId });
            modelBuilder.Entity<QuizDetail>().HasKey(o => new { o.QuizId, o.QuestionId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ClassLevel> ClassLevels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<DifficultLevel> DifficultLevels { get; set; }

        public DbSet<OnlineConnection> OnlineConnections { get; set; }
        public DbSet<Ask> Asks { get; set; }
        public DbSet<Reply> Replies { get; set; }

        public DbSet<Media> Medias { get; set; }
        public DbSet<MediaServer> MediaServers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Hint> Hints { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<QuizActivity> QuizActivities { get; set; }

        public DbSet<LearnActivity> LearnActivities { get; set; }

    }
}

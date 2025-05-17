using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentExamSystem.Data
{
    public class DataBaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<MCQAnswerOptions> MCQAnswerOptions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExamQuestion>().HasKey(eq => new { eq.ExamID, eq.QuestionID });
            builder.Entity<StudentExam>().HasKey(se => new { se.ExamID, se.StudentID });
            builder.Entity<StudentAnswer>().HasKey(sa => new { sa.StudentID, sa.ExamID, sa.QuestionID });
            builder.Entity<MCQAnswerOptions>().HasKey(ma => new { ma.Option, ma.QuestionID });

            base.OnModelCreating(builder);
        }
    }
}

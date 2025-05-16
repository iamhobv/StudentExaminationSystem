using Microsoft.AspNetCore.Identity;

namespace StudentExamSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Exam>? Exams { get; set; }
        public List<StudentAnswer>? StudentAnswers { get; set; }
        public List<StudentExam>? StudentExams { get; set; }
    }
}

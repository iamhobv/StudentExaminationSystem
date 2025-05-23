using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.Models
{
    public class Exam : BaseModel
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }

        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }

        public ApplicationUser? Teacher { get; set; }

        public List<ExamQuestion>? ExamQuestions { get; set; }
        public List<StudentAnswer>? StudentAnswers { get; set; }
        public List<StudentExam>? StudentExams { get; set; }



    }
}

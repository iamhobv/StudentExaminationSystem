using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.Models
{
    public class StudentExam : BaseModel
    {
        [ForeignKey("Student")]
        public string StudentID { get; set; }
        public ApplicationUser? Student { get; set; }

        [ForeignKey("Exam")]
        public int ExamID { get; set; }
        public Exam? Exam { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }

    }
}

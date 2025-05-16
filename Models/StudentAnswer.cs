using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.Models
{
    public class StudentAnswer : BaseModel
    {
        [ForeignKey("Student")]
        public string StudentID { get; set; }
        public ApplicationUser? Student { get; set; }


        [ForeignKey("Exam")]
        public int ExamID { get; set; }
        public Exam? Exam { get; set; }


        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public Question? Question { get; set; }

        public string StudentQuestionAnswer { get; set; }
    }
}

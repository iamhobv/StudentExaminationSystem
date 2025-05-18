using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.Models
{
    public class ExamQuestion : BaseModel
    {
        [ForeignKey("Exam")]
        public int ExamID { get; set; }
        public Exam? Exam { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public Question? Question { get; set; }
    }
}

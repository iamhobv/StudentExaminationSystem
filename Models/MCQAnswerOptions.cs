using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.Models
{
    public class MCQAnswerOptions : BaseModel
    {
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public Question? Question { get; set; }


        public string Option { get; set; }
    }
}

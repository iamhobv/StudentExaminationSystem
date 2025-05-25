using StudentExamSystem.Models;
using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class QuestionDTO : BaseModel
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMark { get; set; }
        public List<string>? Options { get; set; }   
        public string CorrectAnswer { get; set; }

        public string? StudentQuestionAnswer { get; set; }

    }
}

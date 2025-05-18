using StudentExamSystem.Models;
using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class QuestionDTO : BaseModel
    public class QuestionDTO
    {
        public string QuestionBody { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMark { get; set; }
        public string CorrectAnswer { get; set; }

        public string StudentQuestionAnswer { get; set; }

        public int Id { get; set; }
        public string QuestionText { get; set; }
    }
}

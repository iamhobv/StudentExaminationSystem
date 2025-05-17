using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class CreateQuestionDTO
    {
        public string QuestionBody { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMark { get; set; }
        public string CorrectAnswer { get; set; }
    }
}

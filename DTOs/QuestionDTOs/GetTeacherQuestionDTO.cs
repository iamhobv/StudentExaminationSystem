
namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class GetTeacherQuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMark { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string>? Options { get; set; }
    }
}

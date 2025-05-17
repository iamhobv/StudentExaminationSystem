using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class TakeExamDTO
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMark { get; set; }
        public string ExamTitle { get; set; }
    }
}

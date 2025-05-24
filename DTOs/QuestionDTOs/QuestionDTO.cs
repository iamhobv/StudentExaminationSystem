namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class QuestionDTO : BaseModel
    {
        public string QuestionBody { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMark { get; set; }
        public string CorrectAnswer { get; set; }

        public string? StudentQuestionAnswer { get; set; }

    }
}

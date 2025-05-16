using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.Models
{
    public class Question : BaseModel
    {
        public string QuestionBody { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMark { get; set; }
        public string CorrectAnswer { get; set; }
        public List<ExamQuestion>? ExamQuestions { get; set; }
        public List<MCQAnswerOptions>? MCQAnswerOptions { get; set; }
        public List<StudentAnswer>? StudentAnswers { get; set; }


    }
}

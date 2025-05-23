using static StudentExamSystem.Enums.QuestionType;

namespace StudentExamSystem.DTOs.Student
{
    public class TakeExamDTO
    {
        public string ExamTitle { get; set; }

        public List<GetQuestionDTO> Questions { get; set; }
    }
}


namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class AddExamDTO
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }

        public string TeacherId { get; set; }

        public List<int> ExamQuestionsIDs { get; set; }

        //public List<QuestionDTO>? ExamQuestionsTexts { get; set; }
    }
}

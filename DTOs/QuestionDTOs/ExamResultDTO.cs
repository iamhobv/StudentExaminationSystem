using StudentExamSystem.Models;

namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class ExamResultDTO
    {
        public string Title { get; set; }
        public TimeOnly Duration { get; set; }
        public int TotalScore { get; set; }
        public List<QuestionDTO> questions { get; set; }
    }
}


namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class AddExamDTO
    {
        public string Title { get; set; }
        public TimeOnly Duration { get; set; }

        public string TeacherId { get; set; }

        public List<ExamQuestion>? ExamQuestions { get; set; }
    }
}

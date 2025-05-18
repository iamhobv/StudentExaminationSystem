namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class updateExamDTO
    {
        public string? Title { get; set; }
        public TimeOnly? Duration { get; set; }

        public List<ExamQuestionDTO>? ExamQuestions { get; set; }
    }
}

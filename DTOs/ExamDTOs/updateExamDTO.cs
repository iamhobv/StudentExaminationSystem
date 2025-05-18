namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class updateExamDTO
    {
        public int ExamId { get; set; }
        public string? Title { get; set; }
        public TimeOnly? Duration { get; set; }
        public List<int>? QuestionID { get; set; }
    }
}

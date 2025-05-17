namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class GetAvailableExamDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeOnly Duration { get; set; }
    }
}

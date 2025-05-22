namespace StudentExamSystem.DTOs.Student
{
    public class GetAvailableExamDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeOnly Duration { get; set; }
    }
}

namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class StudentExamResultDto
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }
        public double TotalScore { get; set; }
    }
}

namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class ShowResultToTeacherDTO
    {

        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
        public List<StudentExamResultDto> StudentResults { get; set; }
        public int TotalStudents { get; set; }

    }
}

namespace StudentExamSystem.DTOs.Student
{
    public class TakeExamDTO
    {
        public string ExamTitle { get; set; }
        public TimeSpan Duration { get; set; }


        public List<GetQuestionDTO> Questions { get; set; }
    }
}

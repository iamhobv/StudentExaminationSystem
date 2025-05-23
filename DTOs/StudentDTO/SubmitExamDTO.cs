namespace StudentExamSystem.DTOs.Student
{
    public class SubmitExamDTO
    {
        public string StudentID { get; set; }

        public int ExamID { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<StudentAnswerDTO> StudentAnswerDTOList { get; set; }
        //public List<studentExamDTO> studentExamDTOList { get; set; }
    }
}

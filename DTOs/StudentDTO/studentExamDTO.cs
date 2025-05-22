using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.DTOs.Student
{
    public class studentExamDTO
    {
        public string StudentID { get; set; }
        public int ExamID { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}

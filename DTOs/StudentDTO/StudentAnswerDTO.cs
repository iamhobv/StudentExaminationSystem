using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.DTOs.Student
{
    public class StudentAnswerDTO
    {
        public string StudentID { get; set; }

        public int ExamID { get; set; }

        public int QuestionID { get; set; }

        public string StudentQuestionAnswer { get; set; }

    }
}

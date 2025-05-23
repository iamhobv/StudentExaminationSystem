using System.ComponentModel.DataAnnotations.Schema;

namespace StudentExamSystem.DTOs.ExamDTOs
{
    public class ExamDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string TeacherId { get; set; }
        public List<int> QuestionIds { get; set; }

    }
}

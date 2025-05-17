
namespace StudentExamSystem.DTOs.QuestionDTOs
{
    public class EditQeustionDTO
    {
        public int QeustionId { get; set; }
        public string? QuestionBody { get; set; }
        public int? QuestionMark { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}

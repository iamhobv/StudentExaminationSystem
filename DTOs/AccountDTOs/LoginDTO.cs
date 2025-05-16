using System.ComponentModel.DataAnnotations;

namespace StudentExamSystem.DTOs.AccountDTOs
{
    public class LoginDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        //public bool IsTeacher { get; set; }

    }
}

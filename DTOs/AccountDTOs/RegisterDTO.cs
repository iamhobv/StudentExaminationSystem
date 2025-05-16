﻿using System.ComponentModel.DataAnnotations;

namespace StudentExamSystem.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{1,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(\\+2)?(010|011|012|015)[0-9]{8}$", ErrorMessage = "Please enter a valid phone number starting with +20 or 01 followed by exactly 8 digits")]
        public string PhoneNumber { get; set; }
        public bool IsTeacher { get; set; }
    }
}

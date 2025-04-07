using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Dto.AccountDto
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Dto.AccountDto
{
    public class DeleteUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

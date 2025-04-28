using System.ComponentModel.DataAnnotations;

namespace NewsPortal.DTOs
{
    public class RegisterUserDtos
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}

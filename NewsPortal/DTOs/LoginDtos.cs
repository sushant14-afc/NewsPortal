using System.ComponentModel.DataAnnotations;

namespace NewsPortal.DTOs
{
    public class LoginDtos
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}

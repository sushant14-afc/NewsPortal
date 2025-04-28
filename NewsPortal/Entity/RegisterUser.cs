using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Entity
{
    public class RegisterUser
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        
    }
}

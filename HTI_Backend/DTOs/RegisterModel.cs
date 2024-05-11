using System.ComponentModel.DataAnnotations;

namespace HTI_Backend.DTOs
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email  { get; set; }

        
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string  Role { get; set; } 

    }
}

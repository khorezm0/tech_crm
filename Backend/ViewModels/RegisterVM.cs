using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Backend.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

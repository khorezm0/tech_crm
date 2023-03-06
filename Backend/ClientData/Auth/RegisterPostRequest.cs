using System.ComponentModel.DataAnnotations;

namespace Backend.ClientData.Auth
{
    public class RegisterPostRequest
    {
        [Required] public string Login { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

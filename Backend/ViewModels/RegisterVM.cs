using Microsoft.AspNetCore.Identity;

namespace Backend.ViewModels
{
    public class RegisterVM
    {
        public string? Login { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? AccountType { get; set; }
        public string? Password { get; set; }
    }
}

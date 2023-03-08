using System.ComponentModel.DataAnnotations;

namespace Backend.ClientData.Auth
{
    public class LoginPostRequest
    {
        //public string? Id { get; set;}
        [Required]
        public string UserName { get; set; } = String.Empty;
        
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}

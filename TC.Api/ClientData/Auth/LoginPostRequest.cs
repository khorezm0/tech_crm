using System.ComponentModel.DataAnnotations;

namespace TC.Api.ClientData.Auth
{
    public class LoginPostRequest
    {
        [Required]
        public string UserName { get; set; } = String.Empty;
        
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}

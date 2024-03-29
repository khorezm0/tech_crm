﻿using System.ComponentModel.DataAnnotations;

namespace TC.Api.ClientData.Auth
{
    public class RegisterPostRequest
    {
        [Required] public string Login { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must contains at least 6 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace CodingExam.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

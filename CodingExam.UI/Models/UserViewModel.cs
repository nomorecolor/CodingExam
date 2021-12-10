using System.ComponentModel.DataAnnotations;

namespace CodingExam.UI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public InterestViewModel Interest { get; set; }
    }
}

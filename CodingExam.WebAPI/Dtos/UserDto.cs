using System.ComponentModel.DataAnnotations;

namespace CodingExam.WebAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public InterestDto Interest { get; set; }
    }
}

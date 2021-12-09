using CodingExam.Common;

namespace CodingExam.Domain.Models
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get => _password; set => _password = Helpers.Encrypt(value); }
        public Interest Interest { get; set; }

        private string _password;
    }
}

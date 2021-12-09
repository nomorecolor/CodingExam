using System;

namespace CodingExam.Domain.Models
{
    public class UserToken : Entity
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

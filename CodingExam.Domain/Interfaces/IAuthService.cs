using CodingExam.Domain.Models;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateAccessToken(string username);
        Task<string> GenerateRefreshToken();
        Task<bool> ValidateLogin(User user);
    }
}

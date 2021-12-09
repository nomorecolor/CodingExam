using CodingExam.Domain.Models;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateLogin(User user);
    }
}

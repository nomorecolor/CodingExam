using CodingExam.Domain.Models;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsername(string username);
    }
}

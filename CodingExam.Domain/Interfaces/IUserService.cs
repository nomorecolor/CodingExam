using CodingExam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<bool> Delete(User user);
    }
}

using CodingExam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IUserTokenService
    {
        Task<List<UserToken>> GetAll();
        Task<UserToken> GetById(int id);
        Task<UserToken> GetByUserId(int id);
        Task<UserToken> GetByToken(string token);
        Task<UserToken> Add(UserToken entity);
        Task<UserToken> Update(UserToken entity);
        Task<bool> Delete(UserToken entity);
    }
}

using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.Domain.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenService(IUserTokenRepository userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
        }

        public async Task<List<UserToken>> GetAll()
        {
            return await _userTokenRepository.GetAll();
        }

        public async Task<UserToken> GetById(int id)
        {
            return await _userTokenRepository.GetById(id);
        }

        public async Task<UserToken> GetByUserId(int id)
        {
            return await _userTokenRepository.GetByUserId(id);
        }

        public async Task<UserToken> GetByToken(string token)
        {
            return await _userTokenRepository.GetByToken(token);
        }

        public async Task<UserToken> Add(UserToken entity)
        {
            await _userTokenRepository.Add(entity);

            return entity;
        }

        public async Task<UserToken> Update(UserToken entity)
        {
            if (!(await _userTokenRepository.Search(u => u.Id == entity.Id)).Any())
                return null;

            await _userTokenRepository.Add(entity);

            return entity;
        }

        public async Task<bool> Delete(UserToken entity)
        {
            await _userTokenRepository.Delete(entity);

            return true;
        }
    }
}

using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _userRepository.GetByUsername(username);
        }

        public async Task<User> Add(User entity)
        {
            await _userRepository.Add(entity);

            return entity;
        }

        public async Task<User> Update(User entity)
        {
            if (!(await _userRepository.Search(u => u.Id == entity.Id)).Any())
                return null;

            await _userRepository.Update(entity);

            return entity;
        }

        public async Task<bool> Delete(User entity)
        {
            await _userRepository.Delete(entity);

            return true;
        }
    }
}

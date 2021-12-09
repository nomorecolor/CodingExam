using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.Infrastructure.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CodingExamContext _context;

        public AuthRepository(CodingExamContext context)
        {
            _context = context;
        }

        public Task<bool> ValidateLogin(User user)
        {
            return Task.FromResult(_context.Users.Any(u => u.Username == user.Username && u.Password == user.Password));
        }
    }
}

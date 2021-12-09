using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CodingExamContext context) : base(context)
        {
        }

        public override async Task<User> GetById(int id)
        {
            return await Db.Users
                           .Where(u => u.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await Db.Users
                           .Where(u => u.Username == username)
                           .FirstOrDefaultAsync();
        }
    }
}

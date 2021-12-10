using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.Infrastructure.Repositories
{
    public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(CodingExamContext context) : base(context)
        {
        }

        public async Task<UserToken> GetByUserId(int id)
        {
            return await Db.UserTokens
                           .Include(ut => ut.User)
                           .Where(ut => ut.Id == id && ut.RefreshTokenExpiryTime > DateTime.Now)
                           .FirstOrDefaultAsync();
        }

        public async Task<UserToken> GetByToken(string token)
        {
            return await Db.UserTokens
                           .Include(ut => ut.User)
                           .Where(ut => ut.RefreshToken == token && ut.RefreshTokenExpiryTime > DateTime.Now)
                           .FirstOrDefaultAsync();
        }
    }
}

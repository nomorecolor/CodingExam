using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.Infrastructure.Repositories
{
    public class InterestRepository : Repository<Interest>, IInterestRepository
    {
        public InterestRepository(CodingExamContext context) : base(context) { }

        public override async Task<List<Interest>> GetAll()
        {
            return await Db.Interests
                           .Include(i => i.User)
                           .Include(i => i.InterestDetails)
                           .OrderBy(i => i.Id)
                           .ToListAsync();
        }

        public override async Task<Interest> GetById(int id)
        {
            return await Db.Interests
                           .Include(i => i.User)
                           .Include(i => i.InterestDetails)
                           .Where(i => i.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<Interest> GetByUserId(int id)
        {
            return await Db.Interests
                           .Include(i => i.User)
                           .Include(i => i.InterestDetails)
                           .Where(i => i.UserId == id)
                           .FirstOrDefaultAsync();
        }

        public override async Task Update(Interest entity)
        {
            var interestDetails = Db.InterestDetails.Where(id => id.InterestId == entity.Id)
                                                    .ToList();

            Db.InterestDetails.RemoveRange(interestDetails);
            Db.InterestDetails.AddRange(entity.InterestDetails);

            await base.Update(entity);
        }
    }
}

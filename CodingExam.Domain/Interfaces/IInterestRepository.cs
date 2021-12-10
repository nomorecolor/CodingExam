using CodingExam.Domain.Models;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IInterestRepository : IRepository<Interest>
    {
        Task<Interest> GetByUserId(int id);
    }
}

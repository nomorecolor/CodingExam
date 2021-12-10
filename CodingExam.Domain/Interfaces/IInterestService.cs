using CodingExam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IInterestService
    {
        Task<List<Interest>> GetAll();
        Task<Interest> GetById(int id);
        Task<Interest> GetByUserId(int id);
        Task<Interest> Add(Interest entity);
        Task<Interest> Update(Interest entity);
        Task<bool> Delete(Interest entity);
        Task<List<Interest>> Search(int id);
        void GenerateInterestDetails(Interest entity);
    }
}

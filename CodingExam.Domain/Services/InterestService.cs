using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExam.Domain.Services
{
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _interestRepository;

        public InterestService(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public async Task<List<Interest>> GetAll()
        {
            return await _interestRepository.GetAll();
        }

        public async Task<Interest> GetById(int id)
        {
            return await _interestRepository.GetById(id);
        }

        public async Task<Interest> GetByUserId(int id)
        {
            return await _interestRepository.GetByUserId(id);
        }

        public async Task<Interest> Add(Interest entity)
        {
            //var interestSearch = await Search(entity.Id);

            //if ((await _interestRepository.Search(u => u.Id == entity.Id)).Any())
            //    return null;

            //GenerateInterestDetails(entity);

            await _interestRepository.Add(entity);

            return entity;
        }

        public async Task<Interest> Update(Interest entity)
        {
            //var interestSearch = await Search(entity.Id);

            if (!(await _interestRepository.Search(u => u.Id == entity.Id)).Any())
                return null;

            //GenerateInterestDetails(entity);

            await _interestRepository.Update(entity);

            return entity;
        }

        public async Task<bool> Delete(Interest entity)
        {
            await _interestRepository.Delete(entity);

            return true;
        }

        public async Task<List<Interest>> Search(int id)
        {
            return await _interestRepository.Search(u => u.Id == id);
        }
    }
}

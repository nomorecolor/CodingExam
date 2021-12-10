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

        public void GenerateInterestDetails(Interest entity)
        {
            var currentValue = entity.PresentValue;
            var currentRate = entity.LowerBoundInterestRate;

            for (int x = 1; x <= entity.MaturityYears; x++)
            {
                var detail = new InterestDetails
                {
                    Year = x,
                    PresentValue = currentValue,
                    InterestRate = currentRate,
                    FutureValue = (currentValue * (1 + (currentRate / 100)))
                };

                var futureRate = currentRate + entity.IncrementalRate;

                // Add incremental rate to current rate
                if (futureRate <= entity.UpperBoundInterestRate)
                    currentRate = futureRate;
                // Max out rate to upper bound
                else
                    currentRate = entity.UpperBoundInterestRate;

                currentValue = detail.FutureValue;

                entity.InterestDetails.Add(detail);
            }
        }
    }
}

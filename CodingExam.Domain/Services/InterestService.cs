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

        public async Task<Interest> Add(Interest entity)
        {
            var interestSearch = await Search(entity);

            if (interestSearch != null)
                return null;

            GenerateInterestDetails(entity);

            await _interestRepository.Add(entity);

            return entity;
        }

        public async Task<Interest> Update(Interest entity)
        {
            var interestSearch = await Search(entity);

            if (interestSearch == null)
                return null;

            GenerateInterestDetails(entity);

            await _interestRepository.Update(entity);

            return entity;
        }

        public async Task<bool> Delete(Interest entity)
        {
            await _interestRepository.Delete(entity);

            return true;
        }

        private async Task<Interest> Search(Interest entity)
        {
            return (await _interestRepository.Search(u => u.Id == entity.Id)).FirstOrDefault();
        }

        private void GenerateInterestDetails(Interest interest)
        {
            var currentValue = interest.PresentValue;
            var currentRate = interest.LowerBoundInterestRate;

            for (int x = 1; x <= interest.MaturityYears; x++)
            {
                var detail = new InterestDetails
                {
                    Year = x,
                    PresentValue = currentValue,
                    InterestRate = currentRate,
                    FutureValue = (currentValue * (1 + (currentRate / 100)))
                };

                var futureRate = currentRate + interest.IncrementalRate;

                // Max out rate to upper bound
                if ((futureRate - currentRate) < interest.IncrementalRate)
                    currentRate = interest.UpperBoundInterestRate;
                // Add incremental rate to current rate
                else if (futureRate <= interest.UpperBoundInterestRate)
                    currentRate = futureRate;

                currentValue = detail.FutureValue;

                interest.InterestDetails.Add(detail);
            }
        }
    }
}

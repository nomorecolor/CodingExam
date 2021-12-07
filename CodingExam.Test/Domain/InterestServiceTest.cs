using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.Domain.Services;
using CodingExam.Test.Data;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodingExam.Test.Domain
{
    public class InterestServiceTest
    {
        private readonly Mock<IInterestRepository> _interestRepositoryMock;
        private readonly InterestService _interestService;

        public InterestServiceTest()
        {
            _interestRepositoryMock = new Mock<IInterestRepository>();
            _interestService = new InterestService(_interestRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnList_WhenInterestsExist()
        {
            _interestRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(InterestData.InterestList);

            var result = await _interestService.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count == 2);
            Assert.IsType<List<Interest>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyList_WhenInterestsDoNotExist()
        {
            _interestRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Interest>());

            var result = await _interestService.GetAll();

            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.True(result.Count == 0);
            Assert.IsType<List<Interest>>(result);
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void GetById_ShouldReturnData_WhenInterestExist(Interest interest)
        {
            _interestRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(interest);

            var result = await _interestService.GetById(interest.Id);

            Assert.NotNull(result);
            Assert.Equal(interest.Id, result.Id);
            Assert.Equal(interest.PresentValue, result.PresentValue);
            Assert.Equal(interest.LowerBoundInterestRate, result.LowerBoundInterestRate);
            Assert.Equal(interest.UpperBoundInterestRate, result.UpperBoundInterestRate);
            Assert.Equal(interest.IncrementalRate, result.IncrementalRate);
            Assert.Equal(interest.MaturityYears, result.MaturityYears);
            Assert.IsType<Interest>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenInterestDoNotExist()
        {
            _interestRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Interest)null);

            var result = await _interestService.GetById(1);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Search_ShouldReturnList_WhenInterestsExist(int id)
        {
            var interestList = InterestData.InterestList;

            _interestRepositoryMock.Setup(x => x.Search(i => i.Id == id)).ReturnsAsync(interestList.Where(i => i.Id == id).ToList());

            var result = await _interestService.Search(id);

            Assert.NotNull(result);
            Assert.IsType<List<Interest>>(result);
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void Add_ShouldAddInterest(Interest interest)
        {
            _interestRepositoryMock.Setup(x => x.Search(i => i.Id == interest.Id)).ReturnsAsync(new List<Interest> { interest });
            _interestRepositoryMock.Setup(x => x.Add(interest));

            var result = await _interestService.Add(interest);

            Assert.NotNull(result);
            Assert.IsType<Interest>(result);
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void Update_ShouldUpdateInterest_WhenInterestExist(Interest interest)
        {
            _interestRepositoryMock.Setup(x => x.Search(i => i.Id == interest.Id)).ReturnsAsync(new List<Interest> { interest });
            _interestRepositoryMock.Setup(x => x.Update(interest));

            var result = await _interestService.Update(interest);

            Assert.NotNull(result);
            Assert.IsType<Interest>(result);
        }

        [Fact]
        public async void Delete_ShouldUpdateTrue_WhenInterestCanBeRemoved()
        {
            var interest = InterestData.InterestList[0];

            var result = await _interestService.Delete(interest);

            Assert.True(result);
        }
    }
}

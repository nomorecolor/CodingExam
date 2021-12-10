using AutoMapper;
using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.Test.Data;
using CodingExam.WebAPI.Controllers;
using CodingExam.WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodingExam.Test.WebAPI
{
    public class InterestsControllerTest
    {
        private readonly Mock<IInterestService> _interestServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly InterestsController _interestsController;

        public InterestsControllerTest()
        {
            _interestServiceMock = new Mock<IInterestService>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _interestsController = new InterestsController(_interestServiceMock.Object, _userServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnOk_WhenInterestsExist()
        {
            var interests = InterestData.InterestList;
            var dtoExpected = MapObjectToDto(interests);

            _interestServiceMock.Setup(x => x.GetAll()).ReturnsAsync(interests);
            _mapperMock.Setup(x => x.Map<IEnumerable<InterestDto>>(It.IsAny<List<Interest>>())).Returns(dtoExpected);

            var result = await _interestsController.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnOk_WhenInterestsDoNotExist()
        {
            var interests = new List<Interest>();
            var dtoExpected = MapObjectToDto(interests);

            _interestServiceMock.Setup(x => x.GetAll()).ReturnsAsync(interests);
            _mapperMock.Setup(x => x.Map<IEnumerable<InterestDto>>(It.IsAny<List<Interest>>())).Returns(dtoExpected);

            var result = await _interestsController.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetById_ShouldReturnOk_WhenInterestExist(int id)
        {
            var interest = InterestData.InterestList.FirstOrDefault(i => i.Id == id);
            var dtoExpected = MapObjectToDto(interest);

            _userServiceMock.Setup(x => x.GetById(id)).ReturnsAsync(new User());
            _interestServiceMock.Setup(x => x.GetByUserId(id)).ReturnsAsync(interest);
            _mapperMock.Setup(x => x.Map<InterestDto>(It.IsAny<Interest>())).Returns(dtoExpected);

            var result = await _interestsController.GetByUserId(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetById_ShouldReturnBadRequest_WhenUserIsNull(int id)
        {
            var interest = InterestData.InterestList.FirstOrDefault(i => i.Id == id);

            _userServiceMock.Setup(x => x.GetById(id)).ReturnsAsync((User)null);
            _interestServiceMock.Setup(x => x.GetByUserId(id)).ReturnsAsync((Interest)null);

            var result = await _interestsController.GetByUserId(id);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public async void GetById_ShouldReturnNotFound_WhenInterestDoNotExist(int id)
        {
            var interest = InterestData.InterestList.FirstOrDefault(i => i.Id == id);

            _userServiceMock.Setup(x => x.GetById(id)).ReturnsAsync(new User());
            _interestServiceMock.Setup(x => x.GetByUserId(id)).ReturnsAsync((Interest)null);

            var result = await _interestsController.GetByUserId(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void Add_ShouldReturnOk_WhenInterestIsAdded(Interest interest)
        {
            var dtoExpected = MapObjectToDto(interest);

            _mapperMock.Setup(x => x.Map<Interest>(It.IsAny<InterestDto>())).Returns(interest);
            _interestServiceMock.Setup(x => x.Add(interest)).ReturnsAsync(interest);
            _mapperMock.Setup(x => x.Map<InterestDto>(It.IsAny<Interest>())).Returns(dtoExpected);

            var result = await _interestsController.Add(dtoExpected);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            var interestDto = new InterestDto();

            _interestsController.ModelState.AddModelError("PresentValue", "Present Value is required");

            var result = await _interestsController.Add(interestDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequest_WhenInterestIsNull()
        {
            var interest = new Interest();
            var interestDto = MapObjectToDto(interest);

            _mapperMock.Setup(x => x.Map<Interest>(It.IsAny<InterestDto>())).Returns(interest);
            _interestServiceMock.Setup(x => x.Add(interest)).ReturnsAsync((Interest)null);

            var result = await _interestsController.Add(interestDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void Edit_ShouldReturnOk_WhenInterestIsUpdatedCorrectly(Interest interest)
        {
            var interestDto = MapObjectToDto(interest);

            _mapperMock.Setup(x => x.Map<Interest>(It.IsAny<InterestDto>())).Returns(interest);
            _interestServiceMock.Setup(x => x.GetById(interest.Id)).ReturnsAsync(interest);
            _interestServiceMock.Setup(x => x.Add(interest)).ReturnsAsync(interest);

            var result = await _interestsController.Edit(interestDto.Id, interestDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Edit_ShouldReturnBadRequest_WhenInterestIdIsDifferentThanParameterId()
        {
            var interestDto = new InterestDto { Id = 1 };

            var result = await _interestsController.Edit(2, interestDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Edit_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            var interestDto = new InterestDto { Id = 1 };

            _interestsController.ModelState.AddModelError("PresentValue", "Present Value is required");

            var result = await _interestsController.Edit(1, interestDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void Delete_ShouldReturnOk_WhenInterestIsRemoved(Interest interest)
        {
            _interestServiceMock.Setup(x => x.GetById(interest.Id)).ReturnsAsync(interest);
            _interestServiceMock.Setup(x => x.Delete(interest)).ReturnsAsync(true);

            var result = await _interestsController.Delete(interest.Id);

            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public async void Delete_ShouldReturnNotFound_WhenInterestDoNotExist(int id)
        {
            var interest = InterestData.InterestList.FirstOrDefault(i => i.Id == id);

            _interestServiceMock.Setup(x => x.GetById(id)).ReturnsAsync((Interest)null);

            var result = await _interestsController.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        private InterestDto MapObjectToDto(Interest interest)
        {
            return new InterestDto
            {
                Id = interest.Id,
                PresentValue = interest.PresentValue,
                LowerBoundInterestRate = interest.LowerBoundInterestRate,
                UpperBoundInterestRate = interest.UpperBoundInterestRate,
                IncrementalRate = interest.IncrementalRate,
                MaturityYears = interest.MaturityYears,
                InterestDetails = interest.InterestDetails.Select(MapObjectToDto).ToList()
            };
        }

        private List<InterestDto> MapObjectToDto(List<Interest> interestList)
        {
            return interestList.Select(MapObjectToDto).ToList();
        }

        private InterestDetailsDto MapObjectToDto(InterestDetails interestDetails)
        {
            return new InterestDetailsDto
            {
                Id = interestDetails.Id,
                Year = interestDetails.Year,
                PresentValue = interestDetails.PresentValue,
                InterestRate = interestDetails.InterestRate,
                FutureValue = interestDetails.FutureValue,
                InterestId = interestDetails.InterestId
            };
        }
    }
}

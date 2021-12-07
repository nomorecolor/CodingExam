using CodingExam.Domain.Models;
using CodingExam.Infrastructure.Context;
using CodingExam.Infrastructure.Repositories;
using CodingExam.Test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodingExam.Test.Infrastructure
{
    public class InterestRepositoryTest
    {
        private readonly DbContextOptions<CodingExamContext> _options;
        private readonly IConfiguration _config;

        public InterestRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<CodingExamContext>()
                .UseInMemoryDatabase($"CodingExam{Guid.NewGuid()}")
                .Options;

            using (var context = new CodingExamContext(_config, _options))
            {
                context.Interests.AddRange(InterestData.InterestList);
                context.SaveChanges();
            }
        }

        [Fact]
        public async void GetAll_ShouldReturnList_WhenInterestsExist()
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                var interestRepository = new InterestRepository(context);

                var result = await interestRepository.GetAll();

                Assert.NotNull(result);
                Assert.NotEmpty(result);
                Assert.True(result.Count == 2);
                Assert.IsType<List<Interest>>(result);
            }
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyList_WhenInterestsDoNotExist()
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                context.Interests.RemoveRange(context.Interests);
                context.SaveChanges();

                var interestRepository = new InterestRepository(context);

                var result = await interestRepository.GetAll();

                Assert.NotNull(result);
                Assert.Empty(result);
                Assert.True(result.Count == 0);
                Assert.IsType<List<Interest>>(result);
            }
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void GetById_ShouldReturnData_WhenInterestExist(Interest interest)
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                var interestRepository = new InterestRepository(context);

                var result = await interestRepository.GetById(interest.Id);

                Assert.NotNull(result);
                Assert.Equal(interest.Id, result.Id);
                Assert.Equal(interest.PresentValue, result.PresentValue);
                Assert.Equal(interest.LowerBoundInterestRate, result.LowerBoundInterestRate);
                Assert.Equal(interest.UpperBoundInterestRate, result.UpperBoundInterestRate);
                Assert.Equal(interest.IncrementalRate, result.IncrementalRate);
                Assert.Equal(interest.MaturityYears, result.MaturityYears);
                Assert.IsType<Interest>(result);
            }
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenInterestDoNotExist()
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                context.Interests.RemoveRange(context.Interests);
                context.SaveChanges();

                var interestRepository = new InterestRepository(context);

                var result = await interestRepository.GetById(1);

                Assert.Null(result);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Search_ShouldReturnList_WhenInterestsExist(int id)
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                var interestList = InterestData.InterestList;

                var interestRepository = new InterestRepository(context);

                var result = await interestRepository.Search(i => i.Id == id);

                Assert.NotNull(result);
                Assert.IsType<List<Interest>>(result);
            }
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void Add_ShouldAddInterest(Interest interest)
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                context.Interests.RemoveRange(context.Interests);
                context.SaveChanges();

                var interestRepository = new InterestRepository(context);

                await interestRepository.Add(interest);

                var result = context.Interests.FirstOrDefault(i => i.Id == interest.Id);

                Assert.NotNull(result);
                Assert.Equal(interest.Id, result.Id);
                Assert.Equal(interest.PresentValue, result.PresentValue);
                Assert.Equal(interest.LowerBoundInterestRate, result.LowerBoundInterestRate);
                Assert.Equal(interest.UpperBoundInterestRate, result.UpperBoundInterestRate);
                Assert.Equal(interest.IncrementalRate, result.IncrementalRate);
                Assert.Equal(interest.MaturityYears, result.MaturityYears);
                Assert.IsType<Interest>(result);
            }
        }

        [Theory]
        [MemberData(nameof(InterestData.TestingData), MemberType = typeof(InterestData))]
        public async void Update_ShouldUpdateInterest_WhenInterestExist(Interest interest)
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                var interestRepository = new InterestRepository(context);

                interest.PresentValue *= 2;

                await interestRepository.Update(interest);

                var result = context.Interests.FirstOrDefault(i => i.Id == interest.Id);

                Assert.NotNull(result);
                Assert.Equal(interest.Id, result.Id);
                Assert.Equal(interest.PresentValue, result.PresentValue);
                Assert.Equal(interest.LowerBoundInterestRate, result.LowerBoundInterestRate);
                Assert.Equal(interest.UpperBoundInterestRate, result.UpperBoundInterestRate);
                Assert.Equal(interest.IncrementalRate, result.IncrementalRate);
                Assert.Equal(interest.MaturityYears, result.MaturityYears);
                Assert.IsType<Interest>(result);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Delete_ShouldUpdateTrue_WhenInterestCanBeRemoved(int id)
        {
            await using (var context = new CodingExamContext(_config, _options))
            {
                var interestRepository = new InterestRepository(context);

                var toDelete = context.Interests.FirstOrDefault(i => i.Id == id);

                await interestRepository.Delete(toDelete);

                var deleted = context.Interests.FirstOrDefault(i => i.Id == id);

                Assert.Null(deleted);
            }
        }
    }
}

using CodingExam.Domain.Models;
using System.Collections.Generic;

namespace CodingExam.Test.Data
{
    public static class InterestData
    {
        public static IEnumerable<object[]> TestingData =>
            new List<object[]>
            {
                new object[] { InterestList[0] },
                new object[] { InterestList[1] }
            };

        public static List<Interest> InterestList =>
            new List<Interest>
            {
                new Interest { Id = 1, PresentValue = 1000, LowerBoundInterestRate = 10, UpperBoundInterestRate = 50, IncrementalRate = 20, MaturityYears = 4 },
                new Interest { Id = 2, PresentValue = 2000, LowerBoundInterestRate = 30, UpperBoundInterestRate = 100, IncrementalRate = 40, MaturityYears = 8 }
            };
    }
}

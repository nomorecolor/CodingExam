using System.Collections.Generic;

namespace CodingExam.Domain.Models
{
    public class Interest : Entity
    {
        public decimal PresentValue { get; set; }
        public decimal LowerBoundInterestRate { get; set; }
        public decimal UpperBoundInterestRate { get; set; }
        public decimal IncrementalRate { get; set; }
        public int MaturityYears { get; set; }
        public List<InterestDetails> InterestDetails { get; set; }

        public Interest()
        {
            InterestDetails = new List<InterestDetails>();
        }
    }
}

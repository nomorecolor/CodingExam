using CodingExam.WebAPI.Enums;
using CodingExam.WebAPI.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodingExam.WebAPI.Dtos
{
    public class InterestDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Present Value is required")]
        public decimal PresentValue { get; set; }

        [Required(ErrorMessage = "Lower Bound Interest Rate is required")]
        [CompareNumber("UpperBoundInterestRate", CompareNumberEnum.LessThanEqual, ErrorMessage = "Lower Bound Interest Rate must be less than Upper Bound Interest Rate")]
        public decimal LowerBoundInterestRate { get; set; }

        [Required(ErrorMessage = "Upper Bound Interest Rate is required")]
        [CompareNumber("LowerBoundInterestRate", CompareNumberEnum.GreaterThanEqual, ErrorMessage = "Upper Bound Interest Rate must be greater than Lower Bound Interest Rate")]
        public decimal UpperBoundInterestRate { get; set; }

        [Required(ErrorMessage = "Incremental Rate is required")]
        public decimal IncrementalRate { get; set; }

        [Required(ErrorMessage = "Maturity Year(s) is required")]
        public int MaturityYears { get; set; }

        [Required(ErrorMessage = "InterestDetails is required")]
        [MinLength(1, ErrorMessage = "InterestDetails should not be empty")]
        public List<InterestDetailsDto> InterestDetails { get; set; }


        public UserDto User { get; set; }
        public int UserId { get; set; }

        public InterestDto()
        {
            InterestDetails = new List<InterestDetailsDto>();
        }
    }
}

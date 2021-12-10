using CodingExam.Common.Enums;
using CodingExam.Common.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodingExam.UI.Models
{
    public class InterestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Present Value is required")]
        public decimal? PresentValue { get; set; }

        [Required(ErrorMessage = "Lower Bound Interest Rate is required")]
        [CompareNumber("UpperBoundInterestRate", CompareNumberEnum.LessThanEqual, ErrorMessage = "Lower Bound Interest Rate must be less than Upper Bound Interest Rate")]
        public decimal? LowerBoundInterestRate { get; set; }

        [Required(ErrorMessage = "Upper Bound Interest Rate is required")]
        [CompareNumber("LowerBoundInterestRate", CompareNumberEnum.GreaterThanEqual, ErrorMessage = "Upper Bound Interest Rate must be greater than Lower Bound Interest Rate")]
        public decimal? UpperBoundInterestRate { get; set; }

        [Required(ErrorMessage = "Incremental Rate is required")]
        public decimal? IncrementalRate { get; set; }

        [Required(ErrorMessage = "Maturity Year(s) is required")]
        public int? MaturityYears { get; set; }
        public List<InterestDetailsViewModel> InterestDetails { get; set; }


        public UserViewModel User { get; set; }
        public int UserId { get; set; }

        public InterestViewModel()
        {
            InterestDetails = new List<InterestDetailsViewModel>();
        }
    }
}

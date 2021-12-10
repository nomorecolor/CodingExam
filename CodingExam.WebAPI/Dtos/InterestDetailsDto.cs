﻿namespace CodingExam.WebAPI.Dtos
{
    public class InterestDetailsDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public decimal PresentValue { get; set; }
        public decimal InterestRate { get; set; }
        public decimal FutureValue { get; set; }
        public int InterestId { get; set; }
    }
}

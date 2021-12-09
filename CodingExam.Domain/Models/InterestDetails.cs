namespace CodingExam.Domain.Models
{
    public class InterestDetails : Entity
    {
        public int Year { get; set; }
        public decimal PresentValue { get; set; }
        public decimal InterestRate { get; set; }
        public decimal FutureValue { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}

namespace CodingExam.Domain.Models
{
    public class InterestDetails : Entity
    {
        public int Year { get; set; }
        public double PresentValue { get; set; }
        public double InterestRate { get; set; }
        public double FutureValue { get; set; }
        public Interest Interest { get; set; }
    }
}

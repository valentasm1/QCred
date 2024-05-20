namespace QCred.Data.Models;

public interface ICalculateInterestRateResponse
{
    decimal InterestRate { get; set; }
}

public class CalculateInterestRateResponse : ICalculateInterestRateResponse
{
    public decimal InterestRate { get; set; }
}
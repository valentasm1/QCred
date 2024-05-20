namespace QCred.Data.Models;

public interface ICalculateInterestRateRequest
{
    decimal TotalFutureDebt { get; set; }
}

public class CalculateInterestRateRequest : ICalculateInterestRateRequest
{
    public decimal TotalFutureDebt { get; set; }

    public CalculateInterestRateRequest(decimal totalFutureDebt)
    {
        TotalFutureDebt = totalFutureDebt;
    }
}
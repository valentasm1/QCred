namespace QCred.Data.Models.Rules;

public interface IInterestRateRule
{
    Task<ICalculateInterestRateResponse> CalculateInterestRate(ICalculateInterestRateRequest request);
}
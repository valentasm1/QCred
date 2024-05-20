using QCred.Data.Models;
using QCred.Data.Repositories;

namespace QCred.Engine;

public interface IInterestRateCalculationEngine
{
    Task<decimal> CalculateInterestRate(CalculateInterestRateRequest calculateInterestRateRequest);
}

public class InterestRateCalculationEngine : IInterestRateCalculationEngine
{
    private readonly IInterestRateRuleRepository _interestRateRuleRepository;

    public InterestRateCalculationEngine(IInterestRateRuleRepository interestRateRuleRepository)
    {
        _interestRateRuleRepository = interestRateRuleRepository;
    }

    public async Task<decimal> CalculateInterestRate(CalculateInterestRateRequest calculateInterestRateRequest)
    {
        var rules = await _interestRateRuleRepository.GetRules();

        var results = new List<ICalculateInterestRateResponse>();
        foreach (var interestRateRule in rules)
        {
            var response = await interestRateRule.CalculateInterestRate(calculateInterestRateRequest);
            results.Add(response);
        }

        if (!results.Any())
        {
            throw new InvalidOperationException("No interest rate rules found");
        }

        return results.MinBy(x => x.InterestRate)!.InterestRate;
    }
}
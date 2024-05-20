using QCred.Data.Models.Rules;
using QCred.Engine.Validators;

namespace QCred.Data.Repositories;

public interface IInterestRateRuleRepository
{
    Task<IEnumerable<IInterestRateRule>> GetRules();
}

public class InterestRateRuleRepository : IInterestRateRuleRepository
{
    private readonly IList<IInterestRateRule> _rules = new List<IInterestRateRule>();

    public InterestRateRuleRepository()
    {
        _rules.Add(new InterestRateStaticRule());
    }

    public Task<IEnumerable<IInterestRateRule>> GetRules()
    {
        return Task.FromResult<IEnumerable<IInterestRateRule>>(_rules);
    }
}
using QCred.Data.Models.Rules;
using QCred.Engine.Validators;

namespace QCred.Data.Repositories;

public interface ICreditRuleRepository
{
    Task<IEnumerable<ICreditRule>> GetRules();
}

public class CreditRuleRepository : ICreditRuleRepository
{
    private readonly IList<ICreditRule> _rules = new List<ICreditRule>();
    public CreditRuleRepository()
    {
        _rules.Add(new CreditAmountStaticRule());
    }

    public Task<IEnumerable<ICreditRule>> GetRules()
    {
        return Task.FromResult<IEnumerable<ICreditRule>>(_rules);
    }
}
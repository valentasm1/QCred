using QCred.Data.Models;
using QCred.Data.Models.Core;
using QCred.Data.Repositories;

namespace QCred.Engine;

public interface ICreditDecisionEngine
{
    Task<ICreditResponse> Validate(ICreditRequest creditRequest);
}

public class CreditDecisionEngine : ICreditDecisionEngine
{
    private readonly ICreditRuleRepository _ruleRepository;

    public CreditDecisionEngine(ICreditRuleRepository ruleRepository)
    {
        _ruleRepository = ruleRepository;
    }

    public async Task<ICreditResponse> Validate(ICreditRequest creditRequest)
    {
        var rules = await _ruleRepository.GetRules();
        var errors = new List<Error>();
        foreach (var rule in rules)
        {
            var error = await rule.Validate(creditRequest);
            if (error != null)
            {
                errors.Add(error);
            }
        }

        return new CreditResponse()
        {
            Errors = errors
        };
    }
}
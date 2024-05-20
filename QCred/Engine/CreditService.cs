using FluentValidation;
using QCred.Data.Models;
using QCred.Data.Models.Core;

namespace QCred.Engine;

public interface ICreditService
{
    Task<CreditDecision> GetCreditDecisionAsync(CreditApplication application, CancellationToken cancellationToken);
}

public class CreditService : ICreditService
{
    private readonly ICreditDecisionEngine _creditDecisionEngine;
    private readonly IInterestRateCalculationEngine _interestRateCalculationEngine;
    private readonly IValidator<CreditApplication> _creditApplicationValidator;

    public CreditService(
        ICreditDecisionEngine creditDecisionEngine,
        IInterestRateCalculationEngine interestRateCalculationEngine,
        IValidator<CreditApplication> creditApplicationValidator)
    {
        _creditDecisionEngine = creditDecisionEngine;
        _interestRateCalculationEngine = interestRateCalculationEngine;
        _creditApplicationValidator = creditApplicationValidator;
    }

    public async Task<CreditDecision> GetCreditDecisionAsync(CreditApplication application, CancellationToken cancellationToken)
    {
        var validationResult = await _creditApplicationValidator.ValidateAsync(application, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new CreditDecision
            {
                IsSuccess = false,
                Errors = validationResult.Errors.Select(x => new Error { Message = x.ErrorMessage })
            };
        }

        var creditDecisionResponse = await _creditDecisionEngine.Validate(new CreditRequest(application.RequiredCreditAmount));
        if (creditDecisionResponse.Errors.Any())
        {
            return new CreditDecision
            {
                IsSuccess = false,
                Errors = creditDecisionResponse.Errors
            };
        }

        var interestRateRequest = new CalculateInterestRateRequest(application.RequiredCreditAmount + application.CurrentCreditAmount);
        var interestRate = await _interestRateCalculationEngine.CalculateInterestRate(interestRateRequest);

        return new CreditDecision
        {
            IsSuccess = true,
            InterestRate = interestRate
        };
    }
}
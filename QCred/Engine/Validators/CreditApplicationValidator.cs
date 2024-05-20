using FluentValidation;
using QCred.Data.Models;

namespace QCred.Engine.Validators;

public class CreditApplicationValidator : AbstractValidator<CreditApplication>
{
    public CreditApplicationValidator()
    {
        RuleFor(x => x.RequiredCreditAmount).GreaterThan(0);
        RuleFor(x => x.CurrentCreditAmount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.RepaymentInMonths).GreaterThan(0);
    }
}
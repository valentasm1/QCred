using QCred.Data.Models;
using QCred.Data.Models.Core;
using QCred.Data.Models.Rules;

namespace QCred.Engine.Validators;

public class CreditAmountStaticRule : ICreditRule
{
    public Task<Error?> Validate(ICreditRequest creditRequest)
    {
        if (creditRequest.Amount <= 0)
        {
            return Task.FromResult<Error?>(new Error { Message = "Credit amount must be greater than 0" });
        }

        if (creditRequest.Amount < 2000)
        {
            return Task.FromResult<Error?>(new Error { Message = "Credit amount must be greater or equal than 2000" });
        }

        if (creditRequest.Amount > 69000)
        {
            return Task.FromResult<Error?>(new Error { Message = "Credit amount must be lower than 69000" });
        }

        return Task.FromResult<Error?>(null);
    }
}
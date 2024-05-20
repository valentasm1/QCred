using QCred.Data.Models;
using QCred.Data.Models.Rules;

namespace QCred.Engine.Validators;

public class InterestRateStaticRule : IInterestRateRule
{
    public Task<ICalculateInterestRateResponse> CalculateInterestRate(ICalculateInterestRateRequest request)
    {
        var futureDebt = request.TotalFutureDebt;

        if (futureDebt < 20000)
        {
            return Task.FromResult<ICalculateInterestRateResponse>(new CalculateInterestRateResponse { InterestRate = 3m });
        }

        if (futureDebt is >= 20000 and < 40000)
        {
            return Task.FromResult<ICalculateInterestRateResponse>(new CalculateInterestRateResponse { InterestRate = 4m });
        }

        if (futureDebt is >= 40000 and < 60000)
        {
            return Task.FromResult<ICalculateInterestRateResponse>(new CalculateInterestRateResponse { InterestRate = 5m });
        }

        return Task.FromResult<ICalculateInterestRateResponse>(new CalculateInterestRateResponse { InterestRate = 6m });
    }
}
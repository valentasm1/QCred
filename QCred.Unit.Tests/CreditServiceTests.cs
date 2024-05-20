using FluentAssertions;
using QCred.Data.Models;
using QCred.Data.Repositories;
using QCred.Engine;
using QCred.Engine.Validators;

namespace QCred.Unit.Tests;

public class CreditServiceTests
{
    [Fact]
    public async Task Invalid_Inputs_Should_Return_Errors()
    {
        var creditService = CreateCreditService();
        var creditApplication = new CreditApplication
        {
            RequiredCreditAmount = 0,
            CurrentCreditAmount = -1,
            RepaymentInMonths = 0
        };

        var result = await creditService.GetCreditDecisionAsync(creditApplication, CancellationToken.None);
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(3);
        result.Errors.Should().Contain(x => x.Message == "'Required Credit Amount' must be greater than '0'.");
        result.Errors.Should().Contain(x => x.Message == "'Current Credit Amount' must be greater than or equal to '0'.");
        result.Errors.Should().Contain(x => x.Message == "'Repayment In Months' must be greater than '0'.");
    }

    [Fact]
    public async Task Too_Low_Credit_Request()
    {
        var creditService = CreateCreditService();
        var creditApplication = new CreditApplication
        {
            RequiredCreditAmount = 2000 - 0.1m,
            CurrentCreditAmount = 0,
            RepaymentInMonths = 1
        };

        var result = await creditService.GetCreditDecisionAsync(creditApplication, CancellationToken.None);
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(x => x.Message == "Credit amount must be greater or equal than 2000");
    }

    [Fact]
    public async Task Too_High_Credit_Request()
    {
        var creditService = CreateCreditService();
        var creditApplication = new CreditApplication
        {
            RequiredCreditAmount = 69000 + 0.1m,
            CurrentCreditAmount = 0,
            RepaymentInMonths = 1
        };

        var result = await creditService.GetCreditDecisionAsync(creditApplication, CancellationToken.None);
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(x => x.Message == "Credit amount must be lower than 69000");
    }

    [Theory]
    [InlineData(2000, 0, 3)]
    [InlineData(2000, 20000, 4)]
    [InlineData(2000, (20000 - 2000 - 0.01), 3)]
    [InlineData(2000, 40000, 5)]
    [InlineData(2000, 60000, 6)]
    public async Task Test_Interest_Rates(decimal requiredCredit, decimal currentDebt, decimal expectedInterestRate)
    {
        var creditService = CreateCreditService();
        var creditApplication = new CreditApplication
        {
            RequiredCreditAmount = requiredCredit,
            CurrentCreditAmount = currentDebt,
            RepaymentInMonths = 1
        };

        var result = await creditService.GetCreditDecisionAsync(creditApplication, CancellationToken.None);
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.InterestRate.Should().Be(expectedInterestRate);
    }

    private ICreditService CreateCreditService()
    {
        return new CreditService(new CreditDecisionEngine(new CreditRuleRepository()),
            new InterestRateCalculationEngine(new InterestRateRuleRepository()),
            new CreditApplicationValidator());
    }
}
using FluentValidation;
using QCred.Data.Models;
using QCred.Data.Repositories;
using QCred.Engine;

namespace QCred;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddScoped<ICreditService, CreditService>();
        builder.Services.AddTransient<ICreditDecisionEngine, CreditDecisionEngine>();
        builder.Services.AddTransient<IInterestRateCalculationEngine, InterestRateCalculationEngine>();
        builder.Services.AddTransient<ICreditRuleRepository, CreditRuleRepository>();
        builder.Services.AddTransient<IInterestRateRuleRepository, InterestRateRuleRepository>();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapPost("/credit", async (CreditApplication model, ICreditService service) =>
        {
            var creditDecision = await service.GetCreditDecisionAsync(model, default);
            return Results.Json(creditDecision);
        });

        app.Run();
    }
}
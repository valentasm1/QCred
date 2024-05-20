namespace QCred.Data.Models;

public interface ICreditRequest
{
    decimal Amount { get; init; }
}

public class CreditRequest : ICreditRequest
{
    public decimal Amount { get; init; }

    public CreditRequest(decimal amount)
    {
        Amount = amount;
    }
}
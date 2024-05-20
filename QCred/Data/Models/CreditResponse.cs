using QCred.Data.Models.Core;

namespace QCred.Data.Models;

public interface ICreditResponse
{
    IReadOnlyCollection<Error> Errors { get; set; }
}

public class CreditResponse : ICreditResponse
{
    public IReadOnlyCollection<Error> Errors { get; set; } = new List<Error>();
}
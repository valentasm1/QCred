using QCred.Data.Models.Core;

namespace QCred.Data.Models;

public class CreditDecision
{
    public bool IsSuccess { get; set; }
    public IEnumerable<Error>? Errors { get; set; }
    public decimal InterestRate { get; set; }
}
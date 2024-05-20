using QCred.Data.Models.Core;

namespace QCred.Data.Models.Rules;

public interface ICreditRule
{
    Task<Error?> Validate(ICreditRequest creditRequest);
}
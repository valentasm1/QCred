using System.Diagnostics;

namespace QCred.Data.Models.Core;

[DebuggerDisplay("{Message}")]
public class Error
{
    public string? Message { get; set; }
}
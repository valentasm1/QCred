namespace QCred.Data.Models;

public class CreditApplication
{
    public decimal RequiredCreditAmount { get; set; }
    public decimal CurrentCreditAmount { get; set; }
    public int RepaymentInMonths { get; set; }
}
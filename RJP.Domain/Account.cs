namespace RJP.Domain;

public class Account
{
    public Account()
    {
        Transactions = new List<Transaction>();
    }

    public int AccountId { get; set; }
    public double InitialCredit { get; set; }
    public int CutomerId { get; set; }
    public Customer Customer { get; set; }
    public IList<Transaction> Transactions { get; set; }
}

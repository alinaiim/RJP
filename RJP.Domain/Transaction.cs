namespace RJP.Domain;

public class Transaction
{
    public int TransactionId { get; set; }
    public string TransactionName { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
}

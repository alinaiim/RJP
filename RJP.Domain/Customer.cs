namespace RJP.Domain;

public class Customer
{
    public Customer()
    {
        Accounts = new List<Account>();
    }
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<Account> Accounts { get; set; }
}

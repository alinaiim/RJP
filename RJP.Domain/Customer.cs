namespace RJP.Domain;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
}

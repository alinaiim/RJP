using RJP.Application.AccountUseCases;
using RJP.Domain;
using RJP.Domain.Exceptions;
using System.Linq;
using Xunit;

namespace RJP.Tests;

public class OpenAccountTests
{
    [Fact]
    public void Open_Account_Throws_Customer_Does_Not_Exist_Exception()
    {
        var db = Helper.InitializeDatabaseContext(nameof(Open_Account_Throws_Customer_Does_Not_Exist_Exception));
        var mockedService = Helper.setupMockCreateTransactionService();
        
        var service = new OpenAccountService(db, mockedService);
        Assert.ThrowsAsync<CustomerDoesNotExistException>(() => service.Execute(1, 0));
    }

    [Fact]
    public void Open_Account_Creates_New_Account()
    {
        var db = Helper.InitializeDatabaseContext(nameof(Open_Account_Creates_New_Account));
        var mockedService = Helper.setupMockCreateTransactionService();
        var service = new OpenAccountService(db, mockedService);

        var customer = db.Customers.Add(new Customer() { FirstName = "Ali", LastName = "Naim" });
        int oldCount = customer.Entity.Accounts.Count;
        db.SaveChanges();

        var response = service.Execute(customer.Entity.CustomerId, 0).Result;
        Assert.True(response.Success);
        Assert.Equal("Account opened successfully", response.Message);
        Assert.True(db.Accounts.Any());

        int newCount = db.Customers.Find(customer.Entity.CustomerId)!.Accounts.Count();
        Assert.Equal(oldCount + 1, newCount);
    }
}

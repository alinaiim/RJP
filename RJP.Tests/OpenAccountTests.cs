using Microsoft.EntityFrameworkCore;
using Moq;
using RJP.Application;
using RJP.Application.AccountUseCases;
using RJP.Application.TransactionUseCases;
using RJP.DAL;
using RJP.Domain;
using RJP.Domain.Exceptions;
using System.Linq;
using Xunit;

namespace RJP.Tests;

public class OpenAccountTests
{
    private ApplicationDbContext InitializeDatabaseContext(string testName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: $"{ nameof(OpenAccountTests) }.{ testName }").Options;
        var db = new ApplicationDbContext(options);
        return db;
    }

    private ICreateTransactionService setupMockService()
    {
        var mock = new Mock<ICreateTransactionService>();
        Transaction transaction = new Transaction() { TransactionId = 1, TransactionName = "Initial transaction" };
        mock.Setup(m => m.Execute(It.IsAny<int>(), It.IsAny<string>()).Result).Returns(new Response<Transaction>() { Message = "AAAA", Success = true, Payload = transaction });
        return mock.Object;
    }

    [Fact]
    public void Open_Account_Throws_Customer_Does_Not_Exist_Exception()
    {
        var db = InitializeDatabaseContext(nameof(Open_Account_Throws_Customer_Does_Not_Exist_Exception));
        var mockedService = setupMockService();
        
        var service = new OpenAccountService(db, mockedService);
        Assert.ThrowsAsync<CustomerDoesNotExistException>(() => service.Execute(1, 0));
    }

    [Fact]
    public void Open_Account_Creates_New_Account()
    {
        var db = InitializeDatabaseContext(nameof(Open_Account_Creates_New_Account));
        var mockedService = setupMockService();
        var service = new OpenAccountService(db, mockedService);

        var customer = db.Customers.Add(new Customer() { FirstName = "Ali", LastName = "Naim" });
        db.SaveChanges();

        var response = service.Execute(customer.Entity.CustomerId, 0).Result;
        Assert.True(response.Success);
        Assert.Equal("Account opened successfully", response.Message);
        Assert.True(db.Accounts.Any());
    }
}

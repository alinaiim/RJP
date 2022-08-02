using RJP.Application.TransactionUseCases;
using RJP.Domain;
using RJP.Domain.Exceptions;
using System.Linq;
using Xunit;

namespace RJP.Tests;

public class CreateTransactionTests
{
    [Fact]
    public void Create_Transaction_Throws_Account_Does_Not_Exist_Exception()
    {
        var db = Helper.InitializeDatabaseContext(nameof(Create_Transaction_Throws_Account_Does_Not_Exist_Exception));

        var service = new CreateTransactionService(db);
        Assert.ThrowsAsync<AccountDoesNotExistException>(() => service.Execute(1, "ADASF"));
    }

    [Fact]
    public void Create_Transaction_Creates_Transaction_In_Database()
    {
        var db = Helper.InitializeDatabaseContext(nameof(Create_Transaction_Creates_Transaction_In_Database));
        var service = new CreateTransactionService(db);

        var account = db.Accounts.Add(new Account() { InitialCredit = 2 });
        db.SaveChanges();

        var response = service.Execute(account.Entity.AccountId, "Initial Transaction").Result;
        Assert.True(response.Success);
        Assert.Equal("Transaction created successfully", response.Message);
        Assert.True(db.Accounts.Any());
    }

}

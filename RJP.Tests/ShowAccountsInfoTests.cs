using RJP.Application.AccountUseCases;
using RJP.Domain;
using System.Threading.Tasks;
using Xunit;

namespace RJP.Tests;

public class ShowAccountsInfoTests
{
    [Fact]
    public async Task Show_Accounts_Info_Shows_All_Accounts()
    {
        var db = Helper.InitializeDatabaseContext(nameof(Show_Accounts_Info_Shows_All_Accounts));
        var createTransactionServiceMock = Helper.setupMockCreateTransactionService();
        var openAccountService = new OpenAccountService(db, createTransactionServiceMock);

        var customer = db.Customers.Add(new Customer() { FirstName = "Ali", LastName = "Naim" }).Entity;
        db.SaveChanges();
        var response1 = await openAccountService.Execute(customer.CustomerId, 0);
        var response2 = await openAccountService.Execute(customer.CustomerId, 0);
        var response3 = await openAccountService.Execute(customer.CustomerId, 2.21);


        var service = new ShowAccountsInfoService(db);
        var response = await service.Execute(customer.CustomerId);
        Assert.True(response.Success);
        Assert.Equal(3, response.Payload.Count);
    }
}

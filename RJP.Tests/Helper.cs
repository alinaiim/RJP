using Microsoft.EntityFrameworkCore;
using Moq;
using RJP.Application;
using RJP.Application.TransactionUseCases;
using RJP.DAL;
using RJP.Domain;

namespace RJP.Tests;

public static class Helper
{
    public static ApplicationDbContext InitializeDatabaseContext(string testName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: $"{ nameof(OpenAccountTests) }.{ testName }").Options;
        var db = new ApplicationDbContext(options);
        return db;
    }

    public static ICreateTransactionService setupMockCreateTransactionService()
    {
        var mock = new Mock<ICreateTransactionService>();
        Transaction transaction = new Transaction() { TransactionId = 1, TransactionName = "Initial transaction" };
        mock.Setup(m => m.Execute(It.IsAny<int>(), It.IsAny<string>()).Result).Returns(new Response<Transaction>() { Message = "AAAA", Success = true, Payload = transaction });
        return mock.Object;
    }

}

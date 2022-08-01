using RJP.DAL;
using RJP.Domain;
using RJP.Domain.Exceptions;

namespace RJP.Application.TransactionUseCases;

public class CreateTransactionService : ICreateTransactionService
{
    private readonly ApplicationDbContext _context;

    public CreateTransactionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response<Transaction>> Execute(int accountId, string transactionName)
    {
        Response<Transaction> response = new Response<Transaction>();

        try
        {
            Transaction transaction = new Transaction() { TransactionName = transactionName };
            var account = await _context.Accounts.FindAsync(accountId);
            if (account is null)
                throw new AccountDoesNotExistException("Account not found");
            account.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            response.Success = true;
            response.Message = "Transaction created successfully";
            response.Payload = transaction;
        }

        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        return response;
    }
}

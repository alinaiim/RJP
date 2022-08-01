using RJP.Application.TransactionUseCases;
using RJP.DAL;
using RJP.Domain;
using RJP.Domain.Exceptions;

namespace RJP.Application.AccountUseCases;

public class OpenAccountService : IOpenAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly ICreateTransactionService _createTransactionService;

    public OpenAccountService(ApplicationDbContext context, ICreateTransactionService createTransactionService)
    {
        _context = context;
        _createTransactionService = createTransactionService;
    }

    public async Task<Response<Account>> Execute(int customerId, double initialCredit)
    {
        Response<Account> response = new Response<Account>();
        try
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer is null)
                throw new CustomerDoesNotExistException("Customer not found");

            Account account = new Account() { CutomerId = customerId, InitialCredit = initialCredit };
            if (initialCredit != 0)
                await _createTransactionService.Execute(account.AccountId, "Initial Transaction");

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Account opened successfully";
            response.Payload = account;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        return response;
    }

}

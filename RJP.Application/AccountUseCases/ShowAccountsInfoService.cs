using RJP.DAL;
using RJP.Domain;
using RJP.Domain.Exceptions;

namespace RJP.Application.AccountUseCases;

public class ShowAccountsInfoService : IShowAccountsInfoService
{
    private readonly ApplicationDbContext _context;

    public ShowAccountsInfoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response<IList<Account>>> Execute(int customerId)
    {
        Response<IList<Account>> response = new Response<IList<Account>>();
        try
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer is null)
                throw new CustomerDoesNotExistException("Customer not found");
            //var accounts = customer.Accounts; // This did not work I think lazy loading is turned on
            var accounts = _context.Accounts.Where(a => a.CutomerId == customerId).ToList();
            response.Success = true;
            response.Payload = accounts;
            response.Message = "Accounts retrieved successfully";
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        return response;
    }
}

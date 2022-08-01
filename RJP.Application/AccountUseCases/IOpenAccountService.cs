using RJP.Domain;

namespace RJP.Application.AccountUseCases;

public interface IOpenAccountService
{
    Task<Response<Account>> Execute(int customerId, double initialCredit);
}

using RJP.Domain;

namespace RJP.Application.AccountUseCases
{
    public interface IShowAccountsInfoService
    {
        Task<Response<IList<Account>>> Execute(int customerId);
    }
}
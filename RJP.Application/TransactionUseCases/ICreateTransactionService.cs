using RJP.Domain;

namespace RJP.Application.TransactionUseCases
{
    public interface ICreateTransactionService
    {
        Task<Response<Transaction>> Execute(int accountId, string transactionName);
    }
}
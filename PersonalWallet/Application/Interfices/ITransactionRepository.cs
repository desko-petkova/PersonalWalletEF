using PersonalWallet.Domain.Entitys;

namespace PersonalWallet.Application.Interfices
{
    public interface ITransactionRepository
    {
        void Save(Transaction transaction);
        IReadOnlyList<Transaction> GetByAccount(int accountId);
    }
}

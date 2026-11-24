using PersonalWallet.Application.Interfices;
using PersonalWallet.Domain.Entitys;

namespace PersonalWallet.Infrastructure
{
    public class FileTransactionRepository : ITransactionRepository
    {
        private readonly FileStorage storage;

        public FileTransactionRepository(FileStorage storage)
        {
            this.storage = storage;
        }

        public void Save(Transaction transaction)
        {
            if(transaction == null)
        throw new ArgumentNullException(nameof(transaction));

            if (transaction.Id != 0)
                throw new InvalidOperationException(
                    "Транзакцията вече има идентификатор.");

            var db = storage.Load();

            transaction.AssignId(db.NextId);
            db.NextId++;

            db.Transactions.Add(transaction);

            storage.Save(db);
        }

        public IReadOnlyList<Transaction> GetByAccount(int accountId)
        {
            var db = storage.Load();

            var result = db.Transactions
                .Where(t => t.AccountId == accountId)
                .ToList();

            if (result.Count == 0)
                throw new ArithmeticException("Account not found");

            return result;
        }

    }
}

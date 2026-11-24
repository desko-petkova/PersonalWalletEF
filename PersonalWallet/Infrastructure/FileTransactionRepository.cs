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
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            var db = storage.Load();

            var newTransaction = new Transaction(
               // db.NextId++,
                transaction.AccountId,
                transaction.Type,
                transaction.Amount,
                transaction.Date);

            db.Transactions.Add(newTransaction);

            storage.Save(db);
        }

        public IReadOnlyList<Transaction> GetByAccount(int accountId)
        {
            var db = storage.Load();

            //return db.Transactions
            //    .Where(t => t.AccountId == accountId)
            //    .ToList();
            List<Transaction> result = new List<Transaction>();

            foreach (var transaction in db.Transactions)
            {
                if (transaction.AccountId == accountId)
                {
                    result.Add(transaction);
                }
            }

            return result;
        }

    }
}

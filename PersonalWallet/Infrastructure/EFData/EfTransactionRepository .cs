using PersonalWallet.Application.Interfices;
using PersonalWallet.Domain.Entitys;

namespace PersonalWallet.Infrastructure.EFData
{
    public class EfTransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext context;

        public EfTransactionRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Save(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public IReadOnlyList<Transaction> GetByAccount(int accountId)
        {
            return context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToList();
        }
    }
}

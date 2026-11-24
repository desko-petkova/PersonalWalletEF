using PersonalWallet.Application.Interfices;
using PersonalWallet.Domain.Entitys;
using PersonalWallet.Domain.Enums;
using PersonalWallet.Domain.ValueObjects;


namespace PersonalWallet.Application
{
    public class WalletService
    {
        private readonly IAccountRepository accountRepo;
        private readonly ITransactionRepository transactionRepo;

        public WalletService(IAccountRepository accountRepo, ITransactionRepository transactionRepo)
        {
            this.accountRepo = accountRepo;
            this.transactionRepo = transactionRepo;
        }
        public void CreateTransaction(int accountId, TransactionType type, decimal amount)
        {
            var account = accountRepo.GetById(accountId);

            var money = new Money(amount);

            if (type == TransactionType.Income)
            {
                account.Deposit(money);
            }
            else
            {
                account.Withdraw(money);
            }

            accountRepo.Update(account);

            var transaction = new Transaction(
                
                accountId,
                type,
                money,
                DateTime.Now);

            transactionRepo.Save(transaction);
        }
        
        public IReadOnlyList<Transaction> GetTransactions(int accountId)
        {
            return transactionRepo.GetByAccount(accountId);
        }
            

        public void CreateAccount(string name, AccountType type, decimal initialAmount)
        {
            
            var account = new Account(
                
                name,
                type,
                new Money(initialAmount));

            accountRepo.Save(account);
        }
        public IReadOnlyList<Account> GetAccounts()
        {
            return accountRepo.GetAll();
        }
    }
}

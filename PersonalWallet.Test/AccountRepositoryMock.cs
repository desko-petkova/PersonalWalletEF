using NUnit.Framework;
using PersonalWallet.Application;
using PersonalWallet.Application.Interfices;
using PersonalWallet.Domain.Entitys;
using PersonalWallet.Domain.Enums;
using PersonalWallet.Domain.ValueObjects;

namespace PersonalWallet.Test
{
    public class AccountRepositoryMock : IAccountRepository
    {
 
        private readonly Account account;
        private readonly int expectedId;
        private readonly decimal expectedBalance;

        private int getByIdCalls;
        private int updateCalls;

        public AccountRepositoryMock( Account account,int expectedId, decimal expectedBalance)
        {
            this.account = account;
            this.expectedId = expectedId;
            this.expectedBalance = expectedBalance;
        }

        public Account GetById(int id)
        {
            Assert.That(id, Is.EqualTo(expectedId));
            getByIdCalls++;

            return account;
        }

        public void Update(Account updatedAccount)
        {
            Assert.That(updatedAccount, Is.SameAs(account));
            Assert.That(
                updatedAccount.Balance.Amount,
                Is.EqualTo(expectedBalance));

            updateCalls++;
        }
        public void Verify()
        {
            Assert.That(getByIdCalls, Is.EqualTo(1),
                "Save трябва да се извика точно веднъж.");
        }
        public void Save(Account account)
        {
            throw new AssertionException("Неочаквано извикване на Save.");
        }

        public IReadOnlyList<Account> GetAll()
        {
            throw new AssertionException("Неочаквано извикване на GetAll.");
        }

    }

    public class TransactionRepositoryMock : ITransactionRepository
    {
        private readonly int expectedAccountId;
        private readonly TransactionType expectedType;
        private readonly decimal expectedAmount;

        private int saveCalls;

        public TransactionRepositoryMock(
            int expectedAccountId,
            TransactionType expectedType,
            decimal expectedAmount)
        {
            this.expectedAccountId = expectedAccountId;
            this.expectedType = expectedType;
            this.expectedAmount = expectedAmount;
        }

        public void Save(Transaction transaction)
        {
            Assert.That(transaction.AccountId, Is.EqualTo(expectedAccountId));
            Assert.That(transaction.Type, Is.EqualTo(expectedType));
            Assert.That(transaction.Amount.Amount, Is.EqualTo(expectedAmount));

            saveCalls++;
        }

        public void Verify()
        {
            Assert.That(saveCalls, Is.EqualTo(1),
                "Save трябва да се извика точно веднъж.");
        }

        public IReadOnlyList<Transaction> GetByAccount(int accountId)
        {
            throw new AssertionException(
                "Неочаквано извикване на GetByAccount.");
        }
    }

    [TestFixture]
    public class WalletServiceTests
    {
        [Test]
        public void CreateTransaction_WithIncome_UpdatesAccountAndSavesTransaction()
        {
            // Подготовка: реален домейн обект с начален баланс 100.
            var account = new Account(
                "Портфейл",
                AccountType.Cash,
                new Money(100m));

            // Очакваме GetById(5) и Update със салдо 150.
            var accountMock = new AccountRepositoryMock(
                account,
                expectedId: 5,
                expectedBalance: 150m);

            // Очакваме запис на приход 50 за сметка 5.
            var transactionMock = new TransactionRepositoryMock(
                expectedAccountId: 5,
                expectedType: TransactionType.Income,
                expectedAmount: 50m);

            var service = new WalletService(accountMock, transactionMock);

            // Действие: изпълнява се истинският метод на сървиса.
            service.CreateTransaction(5, TransactionType.Income, 50m);

            // Проверка на резултата.
            Assert.That(account.Balance.Amount, Is.EqualTo(150m));

            // Проверка на очакваните взаимодействия.
            accountMock.Verify();
            transactionMock.Verify();
        }
    }
}

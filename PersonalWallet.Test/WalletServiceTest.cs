using Moq;
using NUnit.Framework;
using PersonalWallet.Application;
using PersonalWallet.Application.Interfices;
using PersonalWallet.Domain.Entitys;
using PersonalWallet.Domain.Enums;
using PersonalWallet.Domain.ValueObjects;

namespace PersonalWallet.Test
{
    [TestFixture]
    public class WalletServiceTest
    {
        [Test]
        public void CreateTransaction_WithIncome_UpdatesAndSaves()
        {
            // Подготовка
            var account = new Account(
                "Портфейл",
                AccountType.Cash,
                new Money(100m));

            var accountMock = new Mock<IAccountRepository>();
            var transactionMock = new Mock<ITransactionRepository>();

            accountMock
                .Setup(r => r.GetById(5))
                .Returns(account);

            var service = new WalletService(
                accountMock.Object,
                transactionMock.Object);

            // Действие
            service.CreateTransaction(5, TransactionType.Income, 50m);

            // Проверка на баланса
            Assert.That(account.Balance.Amount, Is.EqualTo(150m));

            // Проверка на взаимодействията
            accountMock.Verify(
                r => r.Update(account),
                Times.Once);

            transactionMock.Verify(
                r => r.Save(It.Is<Transaction>(t =>
                    t.AccountId == 5 &&
                    t.Type == TransactionType.Income &&
                    t.Amount.Amount == 50m)),
                Times.Once);
        }
    }
}

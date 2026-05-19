using PersonalWallet.Domain.Enums;
using PersonalWallet.Domain.ValueObjects;

namespace PersonalWallet.Domain.Entitys
{
    public class Transaction
    {
        public int Id{ get; private set; }
        public int AccountId { get; private set; }

        public Account Account { get; private set; } = null!;
        public TransactionType Type { get; private set; }
        public Money Amount { get; private set; }
        public DateTime Date { get; private set; }
       public Transaction() { }

        public Transaction(int accountId, TransactionType type, Money amount, DateTime date)
        {
           // if (id < 0) throw new ArgumentException("Id must be positive.");
            if (accountId <= 0) throw new ArgumentException("AccountId must be positive.");
            if (amount.Amount <= 0) throw new ArgumentException("Amount must be greater than 0.");

          //  Id = id;
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Date = date;
        }
    }
}

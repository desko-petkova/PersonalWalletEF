using PersonalWallet.Domain.Enums;
using PersonalWallet.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace PersonalWallet.Domain.Entitys
{
    public class Transaction
    {
        [JsonInclude]
        public int Id{ get; private set; }

        [JsonInclude]
        public int AccountId { get; private set; }

        [JsonInclude]
        public Account Account { get; private set; } = null!;

        [JsonInclude]
        public TransactionType Type { get; private set; }

        [JsonInclude]
        public Money Amount { get; private set; }

        [JsonInclude]
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

        public void AssignId(int id)
        {
            if (Id != 0)
                throw new InvalidOperationException(
                    "Транзакцията вече има идентификатор.");

            if (id <= 0)
                throw new ArgumentException(
                    "Идентификаторът трябва да е положителен.");

            Id = id;
        }
    }
}

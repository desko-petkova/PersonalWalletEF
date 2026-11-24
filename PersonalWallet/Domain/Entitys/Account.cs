using PersonalWallet.Domain.Enums;
using PersonalWallet.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace PersonalWallet.Domain.Entitys
{
    public sealed class Account
    {
        [JsonInclude]
        public int Id {get; private set; }

        [JsonInclude]
        public string Name {get; private set; } = string.Empty;

        [JsonInclude]
        public AccountType Type { get; private set; }

        [JsonInclude]
        public Money Balance { get; private set; }

        public ICollection<Transaction> Transactions { get; private set; }
       = new List<Transaction>();

        public Account() { }

        [JsonConstructor]
        public Account(string name,AccountType type, Money balance)
        {
           // if (id < 0) throw new ArgumentException("Id must be positive.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
            
            //Id = id;
            Name = name.Trim();
            Type = type;
            Balance = balance;
        }
      
        public void Deposit(Money amount) 
        {
            if (amount.Amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");

            Balance = Balance.Add(amount);

        }

        public void Withdraw(Money amount) 
        {
            if (amount.Amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");

            Balance = Balance.Subtract(amount);
        }

        public void AssignId(int id)
        {
            if (Id != 0)
                throw new InvalidOperationException(
                    "Сметката вече има идентификатор.");

            if (id <= 0)
                throw new ArgumentException(
                    "Идентификаторът трябва да е положителен.");

            Id = id;
        }

    }
}

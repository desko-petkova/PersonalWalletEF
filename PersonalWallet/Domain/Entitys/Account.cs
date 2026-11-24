using PersonalWallet.Domain.Enums;
using PersonalWallet.Domain.ValueObjects;

namespace PersonalWallet.Domain.Entitys
{
    public sealed class Account
    {
        public int Id {get; private set; }
        public string Name {get; private set; } = string.Empty;
        public AccountType Type { get; private set; }
        public Money Balance { get; private set; }

        public ICollection<Transaction> Transactions { get; private set; }
       = new List<Transaction>();

        public Account() { }

        public Account(string name,AccountType type, Money balance)
        {
           // if (id < 0) throw new ArgumentException("Id must be positive.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
            
           // Id = id;
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

    }
}

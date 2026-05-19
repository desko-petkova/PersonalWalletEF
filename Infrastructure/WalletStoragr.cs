using PersonalWallet.Domain.Entitys;

namespace PersonalWallet.Infrastructure
{
    public class WalletStorage
    {
        public int NextId { get; set; } = 1;

        public List<Account> Accounts { get; set; } = new();

        public List<Transaction> Transactions { get; set; } = new();
    }
}

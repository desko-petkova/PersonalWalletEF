using PersonalWallet.Domain.Entitys;

namespace PersonalWallet.Application.Interfices
{
    public interface IAccountRepository
    {
        Account GetById(int id);
        IReadOnlyList<Account> GetAll();
        void Save(Account account);
        void Update(Account account);
    }
}

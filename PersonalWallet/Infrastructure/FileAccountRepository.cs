using PersonalWallet.Application.Interfices;
using PersonalWallet.Domain.Entitys;

namespace PersonalWallet.Infrastructure
{
    public class FileAccountRepository : IAccountRepository
    {
        private readonly FileStorage storage;

        public FileAccountRepository(FileStorage storage)
        {
            this.storage = storage;
        }

        public Account GetById(int id)
        {
            var db = storage.Load();

            var account = db.Accounts.First(a => a.Id == id);

            if(account == null) throw new Exception("Account not found");

            return account;
            
        }

        public IReadOnlyList<Account> GetAll()
        {
            var db = storage.Load();

            return db.Accounts;
        }

        public void Save(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (account.Id != 0)
                throw new InvalidOperationException(
                    "Използвайте Update за съществуваща сметка.");

            var db = storage.Load();

            account.AssignId(db.NextId);
            db.NextId++;

            db.Accounts.Add(account);

            storage.Save(db);

        }

        public void Update(Account account)
        {

            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (account.Id <= 0)
                throw new ArgumentException(
                    "Сметката трябва да има положителен идентификатор.");

            var db = storage.Load();

            for (int i = 0; i < db.Accounts.Count; i++)
            {
                if (db.Accounts[i].Id == account.Id)
                {
                    db.Accounts[i] = account;
                    storage.Save(db);
                    return;
                }
            }

            throw new InvalidOperationException(
                "Сметката не е намерена.");
        }
    }
}

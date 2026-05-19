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

            // return db.Accounts.First(a => a.Id == id);

            foreach (var account in db.Accounts)
            {
                if(account.Id == id)
                {
                    return account;
                }
            }

            throw new Exception("Account not found");
            
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

            var db = storage.Load();

            if (account.Id == 0)
            {
                if (string.IsNullOrWhiteSpace(account.Name))
                    throw new Exception("Account name is missing before save.");

                var newAccount = new Account(
                  //  db.NextId++,
                    account.Name,
                    account.Type,
                    account.Balance);

                db.Accounts.Add(newAccount);
            }
            else
            {
                bool found = false;

                for (int i = 0; i < db.Accounts.Count; i++)
                {
                    if (db.Accounts[i].Id == account.Id)
                    {
                        db.Accounts[i] = account;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    throw new Exception("Account not found.");
                }
            }
            storage.Save(db);

        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}

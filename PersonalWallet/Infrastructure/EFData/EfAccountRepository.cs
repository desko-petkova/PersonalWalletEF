using Microsoft.EntityFrameworkCore;
using PersonalWallet.Application.Interfices;
using PersonalWallet.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWallet.Infrastructure.EFData
{
    public class EfAccountRepository : IAccountRepository
    {
        private readonly AppDbContext context;

        public EfAccountRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Account GetById(int id)
        {
            var account = context.Accounts
                //.Include(a => a.Transactions)
                .FirstOrDefault(a => a.Id == id);

            if (account == null)
                throw new Exception("Account not found.");

            return account;
        }

        public IReadOnlyList<Account> GetAll()
        {
            return context.Accounts
                //Include(a => a.Transactions)
                .ToList();
        }

        public void Save(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            
                context.Accounts.Add(account);
            

            context.SaveChanges();
        }

        public void Update(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            context.Update(account);
            context.SaveChanges();
        }
    }
}

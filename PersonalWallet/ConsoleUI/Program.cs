using Microsoft.EntityFrameworkCore;
using PersonalWallet.Application;
using PersonalWallet.Application.Interfices;
using PersonalWallet.Infrastructure;
using PersonalWallet.Infrastructure.EFData;

namespace PersonalWallet.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=PersonalWallet;Trusted_Connection=True;TrustServerCertificate=True;";

            //var context = new WalletDbContext(connectionString);

            //IAccountRepository accountRepo = new SqlAccountRepository(context);
            //ITransactionRepository transactionRepo = new SqlTransactionRepository(context);
    //        var options = new DbContextOptionsBuilder<AppDbContext>()
    //.UseSqlServer(
    //    "Server=(localdb)\\MSSQLLocalDB;Database=WalletDb;Trusted_Connection=True;TrustServerCertificate=True;")
    //.Options;

    //        using var context = new AppDbContext(options);

    //        context.Database.EnsureCreated();

    //        IAccountRepository accountRepo = new EfAccountRepository(context);
    //        ITransactionRepository transactionRepo = new EfTransactionRepository(context);

           // var service = new WalletService(accountRepo, transactionRepo);

            var storage = new FileStorage("wallet.json");

            IAccountRepository accountRepo = new FileAccountRepository(storage);
            ITransactionRepository transactionRepo = new FileTransactionRepository(storage);

            var service = new WalletService(accountRepo, transactionRepo);

            var ui = new WalletConsoleUI(service);

            ui.Run();

        }
    }
}

       


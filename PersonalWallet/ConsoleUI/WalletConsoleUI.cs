using PersonalWallet.Application;
using PersonalWallet.Domain.Enums;

namespace PersonalWallet.ConsoleUI
{
    public class WalletConsoleUI
    {
        private readonly WalletService service;

        public WalletConsoleUI(WalletService service)
        {
            this.service = service;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Wallet System ===");
                Console.WriteLine("1 Create account");
                Console.WriteLine("2 Add income");
                Console.WriteLine("3 Add expense");
                Console.WriteLine("4 Show accounts");
                Console.WriteLine("5 Show transactions");
                Console.WriteLine("0 Exit");

                Console.Write("Choose: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateAccount();
                        break;

                    case "2":
                        AddTransaction(TransactionType.Income);
                        break;

                    case "3":
                        AddTransaction(TransactionType.Expense);
                        break;

                    case "4":
                        ShowAccounts();
                        break;

                    case "5":
                        ShowTransactions();
                        break;

                    case "0":
                        running = false;
                        break;
                }
            }
        }
        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void CreateAccount()
        {
            
            Console.Write("Name: ");
            string name = Console.ReadLine();

            foreach (var value in Enum.GetValues(typeof(AccountType)))
            {
                Console.WriteLine($"{(int)value} {value}");
            }

            string input = Console.ReadLine();

            if (!int.TryParse(input, out int typeValue) ||
                !Enum.IsDefined(typeof(AccountType), typeValue))
            {
                Console.WriteLine("Invalid account type.");
                Pause();
                return;
            }

            var type = (AccountType)typeValue;

            Console.Write("Initial amount: ");
            string amountInput = Console.ReadLine();

            if (!decimal.TryParse(amountInput, out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                Pause();
                return;
            }

            try
            {
                service.CreateAccount(name, type, amount);
                Console.WriteLine("Account created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Pause();
        }
        private void ShowAccounts()
        {
            try
            {
                var accounts = service.GetAccounts();

                if (accounts == null)
                {
                    Console.WriteLine("Accounts is null.");
                    Pause();
                    return;
                }

                Console.WriteLine($"Loaded accounts: {accounts.Count}");             

                if (accounts.Count == 0)
                {
                    Console.WriteLine("No accounts found.");
                }
                else
                {
                    foreach (var acc in accounts)
                    {
                        Console.WriteLine($"{acc.Id} | {acc.Name,20} | {acc.Type.ToString(),11} | Balance: {acc.Balance.Amount}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Pause();

        }
        private void AddTransaction(TransactionType type)
        {
            Console.Write("Account Id: ");
            string accountInput = Console.ReadLine();

            Console.Write("Amount: ");
            string amountInput = Console.ReadLine();

            if (!int.TryParse(accountInput, out int accountId))
            {
                Console.WriteLine("Invalid account id.");
                Pause();
                return;
            }

            if (!decimal.TryParse(amountInput, out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                Pause();
                return;
            }

            try
            {
               
                service.CreateTransaction(accountId, type, amount);
                Console.WriteLine("Transaction added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }
        private void ShowTransactions()
        {
            Console.Write("Account Id: ");
            string accountInput = Console.ReadLine();
            if (!int.TryParse(accountInput, out int accountId))
            {
                Console.WriteLine("Invalid account id.");
                Pause();
                return;
            }

            var transactions = service.GetTransactions(accountId);

            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Id} {t.Type} {t.Amount.Amount} {t.Date}");
            }

            Pause();
        }
    }
}

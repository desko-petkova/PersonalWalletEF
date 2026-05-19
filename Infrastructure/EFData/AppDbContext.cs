using Microsoft.EntityFrameworkCore;
using PersonalWallet.Domain.Entitys;


namespace PersonalWallet.Infrastructure.EFData
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=WalletDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(builder =>
            {
                builder.Property(x => x.Type)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                builder.OwnsOne(a => a.Balance, money =>
                {
                    money.Property(m => m.Amount)
                        .HasColumnName("Balance")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();
                });

                builder.HasMany(a => a.Transactions)
                    .WithOne(t => t.Account)
                    .HasForeignKey(t => t.AccountId);
            });
            modelBuilder.Entity<Transaction>(builder =>
            {
                builder.Property(x => x.Type)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

                builder.OwnsOne(t => t.Amount, money =>
                {
                    money.Property(m => m.Amount)
                         .HasColumnName("Amount")
                         .HasColumnType("decimal(18,2)")
                         .IsRequired();
                });
            });

        }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        

        
    }
}

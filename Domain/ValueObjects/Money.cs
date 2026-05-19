namespace PersonalWallet.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; }

        public Money() { }
        public Money(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Сумата не може да бъде отрицателна.");

            Amount = amount;
        }

        public Money Add(Money other)
        {
            return new Money(Amount + other.Amount);
        }

        public Money Subtract(Money other)
        {
            decimal result = Amount - other.Amount;

            if (result < 0) throw new InvalidOperationException("Insufficient funds.");
            return new Money(result);

        }
    }
}

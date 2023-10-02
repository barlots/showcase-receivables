namespace ReceivablesShowcase.Domain.Receivables
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string? CurrencyCode { get; private set; }

        private Money() { }

        public static Money Of(decimal amount, string currencyCode)
        {
            return new Money()
            {
                Amount = amount,
                CurrencyCode = currencyCode
            };
        }

        public static bool operator >=(Money left, Money right)
        {
            if (left.CurrencyCode != right.CurrencyCode)
                throw new BusinessRuleException("Cannot compare money of different currencies");

            return left.Amount >= right.Amount;
        }

        public static bool operator <=(Money left, Money right) => right >= left;

        public static bool operator ==(Money left, Money right)
        {
            if (left.CurrencyCode != right.CurrencyCode)
                throw new BusinessRuleException("Cannot compare money of different currencies");

            return left.Amount == right.Amount;
        }

        public static bool operator !=(Money left, Money right) => !(left == right);
    }
}

namespace ReceivablesShowcase.Domain.Receivables
{
    public class Receivable
    {
        public Guid Id { get; private set; }
        public string Reference { get; private set; }
        public Money PaidAmount { get; private set; }
        public Money OpeningAmount { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ClosedDate { get; private set; }
        public bool Cancelled { get; private set; }
        public DebtorDetails Debtor { get; private set; }

        private Receivable() { }

        public static Receivable Create(
            string reference, 
            Money paidAmount, 
            Money openingAmount, 
            DateTime issueDate, 
            DateTime dueDate, 
            DateTime? closedDate, 
            bool cancelled, 
            DebtorDetails debtor)
        {
            if (debtor == null)
                throw new BusinessRuleException("Receivables debtor must be provided");

            if (string.IsNullOrEmpty(reference))
                throw new BusinessRuleException("Receivables reference must be provided");

            if(openingAmount.Amount <= 0)
                throw new BusinessRuleException("Receivables opening amount must be positive");

            if(paidAmount.Amount < 0)
                throw new BusinessRuleException("Receivables paid amount cannot be negative");

            if(closedDate != null && closedDate < issueDate)
                throw new BusinessRuleException("Receivables issue date must be before closed date");

            if (issueDate > dueDate)
                throw new BusinessRuleException("Receivables issue date must be before due date");

            if (paidAmount.CurrencyCode != openingAmount.CurrencyCode)
                throw new BusinessRuleException("Receivables paid amount must be in the same currency as opening amount");

            if(paidAmount != openingAmount && closedDate != null)
                throw new BusinessRuleException("Unsettled receivables cannot have closed date");

            return new Receivable()
            {
                Id = Guid.NewGuid(),
                Reference = reference,
                PaidAmount = paidAmount,
                OpeningAmount = openingAmount,
                IssueDate = issueDate,
                DueDate = dueDate,
                ClosedDate = closedDate,
                Cancelled = cancelled,
                Debtor = debtor,
            };
        }
    }
}

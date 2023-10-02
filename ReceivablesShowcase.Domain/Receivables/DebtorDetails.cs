namespace ReceivablesShowcase.Domain.Receivables
{
    public class DebtorDetails : ValueObject
    {
        public string Name { get; private set; }
        public string Reference { get; private set; }
        public string? RegistrationNumber { get; private set; }
        public Address? Address { get; private set; }

        private DebtorDetails() { }

        public static DebtorDetails Create(string name, string reference, string? registrationNumber, Address? address)
        {
            if (string.IsNullOrEmpty(name)) 
                throw new BusinessRuleException("Debtor name must be provided");

            if (string.IsNullOrEmpty(reference))
                throw new BusinessRuleException("Debtor reference must be provided");

            return new DebtorDetails { 
                Name = name, 
                Reference = reference, 
                RegistrationNumber = registrationNumber, 
                Address = address 
            };
        }
    }
}

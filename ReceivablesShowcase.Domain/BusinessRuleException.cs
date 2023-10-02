namespace ReceivablesShowcase.Domain
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string details) : base(details) { }
    }
}

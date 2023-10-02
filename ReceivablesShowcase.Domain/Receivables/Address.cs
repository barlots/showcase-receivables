using System;
namespace ReceivablesShowcase.Domain.Receivables
{
    public class Address
    {
        public string? Line1 { get; private set; }
        public string? Line2 { get; private set; }
        public string? Town { get; private set; }
        public string? State { get; private set; }
        public string? Zip { get; private set; }
        public string? CountryCode { get; private set; }

        private Address() { }

        public static Address Create(string? line1, string? line2, string? town, string? state, string? zip, string? countryCode)
        {
            return new Address()
            {
                Line1 = line1,
                Line2 = line2,
                Town = town,
                State = state,
                Zip = zip,
                CountryCode = countryCode
            };
        }
    }
}

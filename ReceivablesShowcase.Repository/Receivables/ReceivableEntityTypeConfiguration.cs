using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceivablesShowcase.Domain.Receivables;

namespace ReceivablesShowcase.Repository.Receivables
{
    internal class ReceivableEntityTypeConfiguration : IEntityTypeConfiguration<Receivable>
    {
        public void Configure(EntityTypeBuilder<Receivable> builder)
        {
            builder.ToTable("Receivables");

            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Reference).IsUnique();

            builder.OwnsOne(e => e.OpeningAmount, e =>
            {
                e.Property(e => e.Amount).HasColumnName("OpeningAmount");
                e.Property(e => e.CurrencyCode).HasColumnName("OpeningAmountCurrencyCode").HasMaxLength(8);
            });

            builder.OwnsOne(e => e.PaidAmount, e =>
            {
                e.Property(e => e.Amount).HasColumnName("PaidAmount");
                e.Property(e => e.CurrencyCode).HasColumnName("PaidAmountCurrencyCode").HasMaxLength(8);
            });

            builder.OwnsOne(e => e.Debtor, e =>
            {
                e.OwnsOne(x => x.Address, x => 
                { 
                    x.Property(xx => xx.Town).HasColumnName("DebtorAddressTown");
                    x.Property(xx => xx.Line1).HasColumnName("DebtorAddressLine1");
                    x.Property(xx => xx.Line2).HasColumnName("DebtorAddressLine2");
                    x.Property(xx => xx.State).HasColumnName("DebtorAddressState");
                    x.Property(xx => xx.CountryCode).HasColumnName("DebtorAddressCountryCode").HasMaxLength(8);
                    x.Property(xx => xx.Zip).HasColumnName("DebtorAddressZip").HasMaxLength(32);
                });

                e.Property(ee => ee.Name).HasColumnName("DebtorName").IsRequired();
                e.Property(ee => ee.Reference).HasColumnName("DebtorReference").IsRequired();
                e.Property(ee => ee.RegistrationNumber).HasColumnName("DebtorRegistrationNumber");
            });
        }
    }
}

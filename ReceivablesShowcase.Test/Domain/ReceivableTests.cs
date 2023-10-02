using FluentAssertions;
using NUnit.Framework;
using ReceivablesShowcase.Domain;
using ReceivablesShowcase.Domain.Receivables;

namespace ReceivablesShowcase.Test.Domain
{
    [TestFixture]
    internal class ReceivableTests
    {
        private DebtorDetails _exampleDebtorDetails = DebtorDetails.Create("d-name", "d-ref", null, null);

        [Test]
        public void Create_ValidParameters_CreatesReceivable()
        {
            var sut = Receivable.Create(
                "REF1",
                Money.Of(100m, "EUR"),
                Money.Of(120.0m, "EUR"),
                new DateTime(2023, 03, 03),
                new DateTime(2023, 03, 03),
                null,
                false,
                _exampleDebtorDetails);

            sut.PaidAmount.Amount.Should().Be(100m);
            sut.PaidAmount.CurrencyCode.Should().Be("EUR");
            sut.OpeningAmount.Amount.Should().Be(120.0m);
            sut.OpeningAmount.CurrencyCode.Should().Be("EUR");
            sut.IssueDate.Should().Be(new DateTime(2023, 03, 03));
            sut.DueDate.Should().Be(new DateTime(2023, 03, 03));
            sut.ClosedDate.Should().BeNull();
            sut.Cancelled.Should().BeFalse();
            sut.Debtor.Should().Be(_exampleDebtorDetails);
        }

        [Test]
        public void Create_EmptyReference_ThrowsBusinessRuleException()
        {
            Action act = () => Receivable.Create(
                null,
                Money.Of(100m, "EUR"),
                Money.Of(120.0m, "EUR"),
                new DateTime(2023, 03, 03),
                new DateTime(2023, 03, 03),
                null,
                false,
                _exampleDebtorDetails);

            act.Should().Throw<BusinessRuleException>();
        }

        [Test]
        public void Create_NegativeOpeningAmount_ThrowsBusinessRuleException()
        {
            Action act = () => Receivable.Create(
                "REF1",
                Money.Of(100m, "EUR"),
                Money.Of(-120.0m, "EUR"),
                new DateTime(2023, 03, 03),
                new DateTime(2023, 03, 03),
                null,
                false,
                _exampleDebtorDetails);

            act.Should().Throw<BusinessRuleException>();
        }

        [Test]
        public void Create_NegativePaidAmount_ThrowsBusinessRuleException()
        {
            Action act = () => Receivable.Create(
                "REF1",
                Money.Of(-100m, "EUR"),
                Money.Of(120.0m, "EUR"),
                new DateTime(2023, 03, 03),
                new DateTime(2023, 03, 03),
                null,
                false,
                _exampleDebtorDetails);

            act.Should().Throw<BusinessRuleException>();
        }

        [Test]
        public void Create_ClosedDateBeforeIssueDate_ThrowsBusinessRuleException()
        {
            Action act = () => Receivable.Create(
                "REF1",
                Money.Of(100m, "EUR"),
                Money.Of(120.0m, "EUR"),
                new DateTime(2023, 03, 03),
                new DateTime(2023, 03, 03),
                new DateTime(2022, 03, 03),
                false,
                _exampleDebtorDetails);

            act.Should().Throw<BusinessRuleException>();
        }

        [Test]
        public void Create_PaidAmountInDifferentCurrencyThanOpeningAmount_ThrowsBusinessRuleException()
        {
            Action act = () => Receivable.Create(
                "REF1",
                Money.Of(100m, "EUR"),
                Money.Of(120.0m, "USD"),
                new DateTime(2023, 03, 03),
                new DateTime(2022, 03, 03),
                null,
                false,
                _exampleDebtorDetails);
            act.Should().Throw<BusinessRuleException>();
        }

        [Test]
        public void Create_UnsettledReceivableWithClosedDate_ThrowsBusinessRuleException()
        {
            Action act = () => Receivable.Create(
                "REF1",
                Money.Of(100m, "EUR"),
                Money.Of(120.0m, "EUR"),
                new DateTime(2023, 03, 03),
                new DateTime(2022, 03, 03),
                new DateTime(2022, 03, 03),
                false,
                _exampleDebtorDetails);
            act.Should().Throw<BusinessRuleException>();
        }
    }
}

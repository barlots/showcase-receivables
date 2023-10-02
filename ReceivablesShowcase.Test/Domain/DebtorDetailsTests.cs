using FluentAssertions;
using NUnit.Framework;
using ReceivablesShowcase.Domain;
using ReceivablesShowcase.Domain.Receivables;

namespace ReceivablesShowcase.Test.Domain
{
    [TestFixture]
    public class DebtorDetailsTests
    {
        [Test]
        public void Create_ValidParameters_CreatesDebtorDetails()
        {
            var sut = DebtorDetails.Create("d-name", "d-ref", null, null);

            sut.Name.Should().Be("d-name");
            sut.Reference.Should().Be("d-ref");
            sut.RegistrationNumber.Should().BeNull();
            sut.Address.Should().BeNull();
        }

        [Test]
        public void Create_EmptyName_ThrowsBusinessRuleException()
        {
            Action act = () => DebtorDetails.Create(null, "d-ref", null, null);
            act.Should().Throw<BusinessRuleException>();
        }

        [Test]
        public void Create_EmptyReference_ThrowsBusinessRuleException()
        {
            Action act = () => DebtorDetails.Create("d-name", null, null, null);
            act.Should().Throw<BusinessRuleException>();
        }
    }
}

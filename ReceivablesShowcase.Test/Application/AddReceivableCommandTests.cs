using NUnit.Framework;
using Moq;
using ReceivablesShowcase.Application.Receivables;
using ReceivablesShowcase.Domain.Receivables;
using ReceivablesShowcase.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using FluentAssertions;

namespace ReceivablesShowcase.Test.Application
{
    [TestFixture]
    public class AddReceivableCommandTests
    {
        private Mock<IReceivablesContextTransaction> _ctxTransactionMock;
        private Mock<IDbContextTransaction> _transactionMock;

        [SetUp]
        public void SetUp()
        {
            _transactionMock = new Mock<IDbContextTransaction>();

            _ctxTransactionMock = new Mock<IReceivablesContextTransaction>();
            _ctxTransactionMock.Setup(e => e.BeginAsync()).ReturnsAsync(_transactionMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _ctxTransactionMock.Reset();
            _transactionMock.Reset();
        }

        [Test]
        public async Task Handler_DuplicatedReference_ThrowsException()
        {
            var repositoryMock = new Mock<IReceivablesRepository>();
            repositoryMock.Setup(e => e.ExistsByReference(It.IsAny<string>())).ReturnsAsync(true);

            var command = new AddReceivableCommand
            {
                Items = new List<AddReceivableCommandEntry>
                {
                    new AddReceivableCommandEntry
                    {
                        Reference = "123",
                        CurrencyCode = "EUR",
                        IssueDate = DateTime.Now,
                        OpeningValue = 100,
                        PaidValue = 0,
                        DueDate = DateTime.Now.AddDays(30),
                        ClosedDate = null,
                        Cancelled = false,
                        DebtorName = "John Doe",
                        DebtorReference = "123",
                        DebtorAddress1 = "Address 1",
                        DebtorAddress2 = "Address 2",
                        DebtorTown = "Town",
                        DebtorState = "State",
                        DebtorZip = "Zip",
                        DebtorCountryCode = "PT",
                        DebtorRegistrationNumber = "123"
                    }
                }
            };

            var sut = new AddReceivableCommandHandler(repositoryMock.Object, _ctxTransactionMock.Object);
            Func<Task> act = async () => await sut.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task Handler_ValidCommand_ReturnsReceivableIds()
        {
            var repositoryMock = new Mock<IReceivablesRepository>();
            repositoryMock.Setup(e => e.ExistsByReference(It.IsAny<string>())).ReturnsAsync(false);
            repositoryMock.Setup(e => e.AddAsync(It.IsAny<Receivable>()));

            var command = new AddReceivableCommand
            {
                Items = new List<AddReceivableCommandEntry>
                {
                    new AddReceivableCommandEntry
                    {
                        Reference = "123",
                        CurrencyCode = "EUR",
                        IssueDate = DateTime.Now,
                        OpeningValue = 100,
                        PaidValue = 0,
                        DueDate = DateTime.Now.AddDays(30),
                        ClosedDate = null,
                        Cancelled = false,
                        DebtorName = "John Doe",
                        DebtorReference = "123",
                        DebtorAddress1 = "Address 1",
                        DebtorAddress2 = "Address 2",
                        DebtorTown = "Town",
                        DebtorState = "State",
                        DebtorZip = "Zip",
                        DebtorCountryCode = "PT",
                        DebtorRegistrationNumber = "123"
                    }
                }
            };

            var sut = new AddReceivableCommandHandler(repositoryMock.Object, _ctxTransactionMock.Object);
            var result = await sut.Handle(command, CancellationToken.None);

            result.Should().HaveCount(1);
            result.First().Should().NotBeEmpty();

            repositoryMock.Verify(e => e.AddAsync(It.IsAny<Receivable>()), Times.Once);
            repositoryMock.Verify(e => e.SaveChangesAsync(), Times.Once);
            _ctxTransactionMock.Verify(e => e.BeginAsync(), Times.Once);
            _transactionMock.Verify(e => e.CommitAsync(CancellationToken.None), Times.Once);
        }
    }
}

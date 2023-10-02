using MediatR;
using ReceivablesShowcase.Domain.Receivables;
using ReceivablesShowcase.Repository;

namespace ReceivablesShowcase.Application.Receivables
{
    public class AddReceivableCommand : IRequest<IEnumerable<Guid>>
    {
        public List<AddReceivableCommandEntry> Items { get; init; }
    }

    public class AddReceivableCommandEntry
    {
        public string Reference { get; init; }
        public string CurrencyCode { get; init; }
        public DateTime IssueDate { get; init; }
        public decimal OpeningValue { get; init; }
        public decimal PaidValue { get; init; }
        public DateTime DueDate { get; init; }
        public DateTime? ClosedDate { get; init; }
        public bool? Cancelled { get; init; }
        public string DebtorName { get; init; }
        public string DebtorReference { get; init; }
        public string? DebtorAddress1 { get; init; }
        public string? DebtorAddress2 { get; init; }
        public string? DebtorTown { get; init; }
        public string? DebtorState { get; init; }
        public string? DebtorZip { get; init; }
        public string DebtorCountryCode { get; init; }
        public string? DebtorRegistrationNumber { get; init; }

    }

    public class AddReceivableCommandHandler : IRequestHandler<AddReceivableCommand, IEnumerable<Guid>>
    {
        private readonly IReceivablesRepository _receivablesRepository;
        private readonly IReceivablesContextTransaction _receivablesTransaction;

        public AddReceivableCommandHandler(
            IReceivablesRepository receivablesRepository,
            IReceivablesContextTransaction receivablesTransaction)
        {
            _receivablesRepository = receivablesRepository;
            _receivablesTransaction = receivablesTransaction;
        }

        public async Task<IEnumerable<Guid>> Handle(AddReceivableCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _receivablesTransaction.BeginAsync();
            var receivableIds = new List<Guid>();

            foreach(var item in request.Items)
            {
                var referenceExists = await _receivablesRepository.ExistsByReference(item.Reference);
                if (referenceExists)
                    throw new Exception("Receivables reference must be unique");

                var address = Address.Create(
                    item.DebtorAddress1,
                    item.DebtorAddress2,
                    item.DebtorTown,
                    item.DebtorState,
                    item.DebtorZip,
                    item.DebtorCountryCode);

                var debtorDetails = DebtorDetails.Create(
                    item.DebtorName,
                    item.DebtorReference,
                    item.DebtorRegistrationNumber,
                    address);

                var openingAmount = Money.Of(item.OpeningValue, item.CurrencyCode);
                var paidAmount = Money.Of(item.PaidValue, item.CurrencyCode);

                var receivable = Receivable.Create(
                    item.Reference,
                    paidAmount,
                    openingAmount,
                    item.IssueDate,
                    item.DueDate,
                    item.ClosedDate,
                    item.Cancelled ?? false,
                    debtorDetails);

                await _receivablesRepository.AddAsync(receivable);
                await _receivablesRepository.SaveChangesAsync();
                receivableIds.Add(receivable.Id);
            }

            await transaction.CommitAsync();
            return receivableIds;
        }
    }
}

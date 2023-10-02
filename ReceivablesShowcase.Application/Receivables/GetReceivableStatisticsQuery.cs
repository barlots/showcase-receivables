using MediatR;
using Microsoft.EntityFrameworkCore;
using ReceivablesShowcase.Repository;

namespace ReceivablesShowcase.Application.Receivables
{
    public class GetReceivableStatisticsQuery : IRequest<ReceivableStatisticsResult>
    {
    }

    public class ReceivableStatisticsResult
    {
        public int InvoicesCount { get; set; }
        public ReceivableStatisticsResultValueRecord[] TotalValueOfOpenInvoices { get; set; }
        public ReceivableStatisticsResultValueRecord[] TotalValueOfClosedInvoices { get; set; }
    }

    public class ReceivableStatisticsResultValueRecord
    {
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }

    public class GetReceivableStatisticsQueryHandler : IRequestHandler<GetReceivableStatisticsQuery, ReceivableStatisticsResult>
    {
        public readonly ReceivablesContext _receivablesContext;

        public GetReceivableStatisticsQueryHandler(ReceivablesContext receivablesContext)
        {
            _receivablesContext = receivablesContext;
        }

        public async Task<ReceivableStatisticsResult> Handle(GetReceivableStatisticsQuery request, CancellationToken cancellationToken)
        {
            var result = await _receivablesContext.Receivables.GroupBy(e => 1)
                .Select(g => new ReceivableStatisticsResult
                {
                    TotalValueOfOpenInvoices = g.Where(e => e.ClosedDate == null)
                        .Select(e => new ReceivableStatisticsResultValueRecord
                        {
                            CurrencyCode = e.OpeningAmount.CurrencyCode,
                            Amount = e.OpeningAmount.Amount
                        }).ToArray(),
                    TotalValueOfClosedInvoices = g.Where(e => e.ClosedDate != null)
                        .Select(e => new ReceivableStatisticsResultValueRecord
                        {
                            CurrencyCode = e.OpeningAmount.CurrencyCode,
                            Amount = e.OpeningAmount.Amount
                        }).ToArray(),
                    InvoicesCount = g.Count(),
                })
                .FirstOrDefaultAsync() ?? new ReceivableStatisticsResult
                {
                    InvoicesCount = 0,
                    TotalValueOfClosedInvoices = Array.Empty<ReceivableStatisticsResultValueRecord>(),
                    TotalValueOfOpenInvoices = Array.Empty<ReceivableStatisticsResultValueRecord>()
                };

            result.TotalValueOfOpenInvoices = result.TotalValueOfOpenInvoices
                .GroupBy(e => e.CurrencyCode)
                .Select(e => new ReceivableStatisticsResultValueRecord
                {
                    CurrencyCode = e.Key,
                    Amount = e.Sum(e => e.Amount)
                }).ToArray();

            result.TotalValueOfClosedInvoices = result.TotalValueOfClosedInvoices
                .GroupBy(e => e.CurrencyCode)
                .Select(e => new ReceivableStatisticsResultValueRecord
                {
                    CurrencyCode = e.Key,
                    Amount = e.Sum(e => e.Amount)
                }).ToArray();

            return result ;
        }

        // Example of proper method implementation for EF Core backed by SQL Server (SQLite doest support SUM of decimals)

        //public async Task<ReceivableStatisticsResult> Handle(GetReceivableStatisticsQuery request, CancellationToken cancellationToken)
        //{
        //    var result = await _receivablesContext.Receivables.GroupBy(e => 1)
        //        .Select(g => new ReceivableStatisticsResult
        //        {
        //            TotalValueOfOpenInvoices = g.Where(e => e.ClosedDate == null)
        //                .GroupBy(e => e.PaidAmount.CurrencyCode)
        //                .Select(e => new ReceivableStatisticsResultValueRecord
        //                {
        //                    CurrencyCode = e.Key,
        //                    Amount = e.Sum(e => e.OpeningAmount.Amount)
        //                }).ToArray(),
        //            TotalValueOfClosedInvoices = g.Where(e => e.ClosedDate != null)
        //                .GroupBy(e => e.PaidAmount.CurrencyCode)
        //                .Select(e => new ReceivableStatisticsResultValueRecord
        //                {
        //                    CurrencyCode = e.Key,
        //                    Amount = e.Sum(e => e.OpeningAmount.Amount)
        //                }).ToArray(),
        //            InvoicesCount = g.Count(),
        //        })
        //        .FirstOrDefaultAsync();
        //
        //    return result ?? new ReceivableStatisticsResult
        //    {
        //        InvoicesCount = 0,
        //        TotalValueOfClosedInvoices = Array.Empty<ReceivableStatisticsResultValueRecord>(),
        //        TotalValueOfOpenInvoices = Array.Empty<ReceivableStatisticsResultValueRecord>()
        //    };
        //}
    }
}

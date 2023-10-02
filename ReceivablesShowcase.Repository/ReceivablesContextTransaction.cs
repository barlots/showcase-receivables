using Microsoft.EntityFrameworkCore.Storage;

namespace ReceivablesShowcase.Repository
{
    public class ReceivablesContextTransaction : IReceivablesContextTransaction
    {
        private readonly ReceivablesContext _context;

        public ReceivablesContextTransaction(ReceivablesContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginAsync() => await _context.Database.BeginTransactionAsync();
    }
}

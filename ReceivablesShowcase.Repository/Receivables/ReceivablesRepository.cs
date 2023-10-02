using Microsoft.EntityFrameworkCore;
using ReceivablesShowcase.Domain.Receivables;

namespace ReceivablesShowcase.Repository.Receivables
{
    internal class ReceivablesRepository : IReceivablesRepository
    {
        private readonly ReceivablesContext _ctx;

        public ReceivablesRepository(ReceivablesContext receivablesContext)
        {
            _ctx = receivablesContext;
        }

        public async Task AddAsync(Receivable meeting)
        {
            await _ctx.Receivables.AddAsync(meeting);
        }

        public async Task AddRangeAsync(IEnumerable<Receivable> receivables)
        {
            await _ctx.Receivables.AddRangeAsync(receivables);
        }

        public async Task<bool> ExistsByReference(string reference)
        {
            return await _ctx.Receivables.AnyAsync(x => x.Reference == reference);
        }

        public async Task<Receivable?> GetByIdAsync(Guid id)
        {
            return await _ctx.Receivables.FindAsync(id);
        }

        public async Task SaveChangesAsync() => await _ctx.SaveChangesAsync();
    }
}

namespace ReceivablesShowcase.Domain.Receivables
{
    public interface IReceivablesRepository
    {
        Task AddAsync(Receivable receivable);
        Task AddRangeAsync(IEnumerable<Receivable> receivables);
        Task<Receivable?> GetByIdAsync(Guid id);
        Task<bool> ExistsByReference(string reference);
        Task SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore.Storage;

namespace ReceivablesShowcase.Repository
{
    public interface IReceivablesContextTransaction
    {
        Task<IDbContextTransaction> BeginAsync();
    }
}

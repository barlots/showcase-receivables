using Microsoft.EntityFrameworkCore;
using ReceivablesShowcase.Domain.Receivables;

namespace ReceivablesShowcase.Repository
{
    public class ReceivablesContext : DbContext
    {
        public DbSet<Receivable> Receivables { get; set; }

        public ReceivablesContext()
        {
        }

        public ReceivablesContext(DbContextOptions<ReceivablesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

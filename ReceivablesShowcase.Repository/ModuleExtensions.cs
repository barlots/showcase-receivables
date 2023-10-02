using Microsoft.Extensions.DependencyInjection;
using ReceivablesShowcase.Domain.Receivables;
using ReceivablesShowcase.Repository.Receivables;

namespace ReceivablesShowcase.Repository
{
    public static class ModuleExtensions
    {
        public static void AddReceivablesRepository(this IServiceCollection services)
        {
            services.AddScoped<IReceivablesRepository, ReceivablesRepository>();
            services.AddScoped<IReceivablesContextTransaction, ReceivablesContextTransaction>();
        }
    }
}

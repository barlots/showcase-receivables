using Microsoft.Extensions.DependencyInjection;
using ReceivablesShowcase.Repository;

namespace ReceivablesShowcase.Application
{
    public static class ModuleExtensions
    {
        public static void AddReceivablesApplication(this IServiceCollection services)
        {
            services.AddReceivablesRepository();
            services.AddMediatR(e => e.RegisterServicesFromAssembly(typeof(ModuleExtensions).Assembly));
        }
    }
}

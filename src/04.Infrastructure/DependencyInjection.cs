using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplierMaintenance.Infrastructure.Persistence;

namespace SupplierMaintenance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;
using SupplierMaintenance.Client.Services.BackEnd;

namespace SupplierMaintenance.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services)
    {
        services.AddTransient<SupplierService>();
        services.AddTransient<CityService>();
        services.AddTransient<ProvinceService>();

        return services;
    }
}

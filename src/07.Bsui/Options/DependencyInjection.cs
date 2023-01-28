using SupplierMaintenance.Bsui.Options.AppInfo;

namespace SupplierMaintenance.Bsui.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddMyOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppInfoOptions>(configuration.GetSection(AppInfoOptions.SectionKey));

        return services;
    }
}

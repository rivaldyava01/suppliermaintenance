namespace SupplierMaintenance.Bsui.Options.AppInfo;

public static class DependencyInjection
{
    public static IServiceCollection AddMyOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppInfoOptions>(configuration.GetSection(AppInfoOptions.SectionKey));

        return services;
    }
}

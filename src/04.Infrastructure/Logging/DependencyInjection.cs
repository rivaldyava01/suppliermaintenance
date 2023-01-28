using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SupplierMaintenance.Infrastructure.Logging.Serilog;
using SupplierMaintenance.Infrastructure.Logging.Simple;

namespace SupplierMaintenance.Infrastructure.Logging;

public static class DependencyInjection
{
    public static IHostBuilder UseLoggingService(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        var loggingOptions = configuration.GetSection(LoggingOptions.SectionKey).Get<LoggingOptions>();

        switch (loggingOptions.Provider)
        {
            case LoggingProvider.Simple:
                hostBuilder.UseSimpleLoggingService();
                break;
            case LoggingProvider.Serilog:
                hostBuilder.UseSerilogLoggingService();
                break;
            default:
                throw new ArgumentException($"Unsupported {nameof(Logging)} {nameof(LoggingOptions.Provider)}: {loggingOptions.Provider}");
        }

        return hostBuilder;
    }
}

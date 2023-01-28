using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SupplierMaintenance.Infrastructure.Logging.Simple;

public static class DependencyInjection
{
    public static IHostBuilder UseSimpleLoggingService(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging((hostContext, loggingBuilder) =>
        {
            loggingBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging:Simple"));
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSimpleConsole(options => options.TimestampFormat = "[HH:mm:ss] ");
        });

        return hostBuilder;
    }
}

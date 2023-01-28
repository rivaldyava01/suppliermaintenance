namespace SupplierMaintenance.Infrastructure.Logging;

public class LoggingOptions
{
    public const string SectionKey = nameof(Logging);

    public string Provider { get; set; } = default!;
}

public static class LoggingProvider
{
    public const string Simple = nameof(Simple);
    public const string Serilog = nameof(Serilog);
}

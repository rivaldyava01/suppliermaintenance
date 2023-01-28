namespace SupplierMaintenance.Infrastructure.Persistence;

public class PersistenceOptions
{
    public const string SectionKey = nameof(Persistence);

    public string Provider { get; set; } = default!;
}

public static class PersistenceProvider
{
    public const string SqlServer = nameof(SqlServer);
}

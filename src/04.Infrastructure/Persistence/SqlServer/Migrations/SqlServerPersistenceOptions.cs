namespace SupplierMaintenance.Infrastructure.Persistence.SqlServer;

public class SqlServerPersistenceOptions
{
    public static readonly string SectionKey = $"{nameof(Persistence)}:{nameof(SqlServer)}";

    public string ConnectionString { get; set; } = default!;
    public bool Seeding { get; set; }
}

using SupplierMaintenance.Application;
using SupplierMaintenance.Infrastructure;
using SupplierMaintenance.Infrastructure.Logging;
using SupplierMaintenance.Infrastructure.Persistence;
using SupplierMaintenance.Infrastructure.Persistence.SqlServer;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLoggingService(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

var persistenceOptions = builder.Configuration.GetSection(PersistenceOptions.SectionKey).Get<PersistenceOptions>();

if (persistenceOptions.Provider == PersistenceProvider.SqlServer)
{
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<SqlServerDbContextInitializer>();
    await initializer.InitializeAsync();

    var sqlServerPersistenceOptions = builder.Configuration.GetSection(SqlServerPersistenceOptions.SectionKey).Get<SqlServerPersistenceOptions>();

    if (sqlServerPersistenceOptions.Seeding)
    {
        await initializer.SeedAsync();
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Infrastructure.Persistence.SqlServer;
using SupplierMaintenance.InfraStructure.Persistence.Common.Constants;

namespace SupplierMaintenance.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var persistenceOptions = configuration.GetSection(PersistenceOptions.SectionKey).Get<PersistenceOptions>();

        if (persistenceOptions.Provider == PersistenceProvider.SqlServer)
        {
            var migrationsAssembly = typeof(SqlServerDbContext).Assembly.FullName;
            var sqlServerPersistenceOptions = configuration.GetSection(SqlServerPersistenceOptions.SectionKey).Get<SqlServerPersistenceOptions>();

            services.AddDbContext<SqlServerDbContext>(options =>
            {
                options.UseSqlServer(sqlServerPersistenceOptions.ConnectionString, builder =>
                {
                    builder.MigrationsAssembly(migrationsAssembly);
                    builder.MigrationsHistoryTable(TableNameFor.EfMigrationsHistory, nameof(SupplierMaintenance));
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

                options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
                options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            });

            services.AddScoped<IDbContext>(provider => provider.GetRequiredService<SqlServerDbContext>());
            services.AddScoped<SqlServerDbContextInitializer>();
        }

        return services;
    }
}

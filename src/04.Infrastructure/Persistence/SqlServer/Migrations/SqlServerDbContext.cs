using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Domain.Entities;
using System.Reflection;

namespace SupplierMaintenance.Infrastructure.Persistence.SqlServer;

public class SqlServerDbContext : DbContext, IDbContext
{
    public DbSet<City> Cities => Set<City>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //foreach (var entry in ChangeTracker.Entries<ICreatable>())
        //{
        //    switch (entry.State)
        //    {
        //        case EntityState.Added:
        //            entry.Entity.CreatedBy = "admin";
        //            entry.Entity.Created = DateTimeOffset.Now;
        //            break;
        //    }
        //}

        //foreach (var entry in ChangeTracker.Entries<IModifiable>())
        //{
        //    switch (entry.State)
        //    {
        //        case EntityState.Modified:
        //            entry.Entity.ModifiedBy = "admin";
        //            entry.Entity.Modified = DateTimeOffset.Now;
        //            break;
        //    }
        //}

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}

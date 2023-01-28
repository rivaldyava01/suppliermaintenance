using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Domain.Entities;

namespace SupplierMaintenance.Application.Services.Persistence;

public interface IDbContext
{
    DbSet<City> Cities { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Province> Provinces { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

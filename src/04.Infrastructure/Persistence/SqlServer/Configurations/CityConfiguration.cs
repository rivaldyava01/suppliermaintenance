using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Domain.Entities;
using SupplierMaintenance.InfraStructure.Persistence.Common.Constants;
using SupplierMaintenance.Shared.Cities.Constants;

namespace SupplierMaintenance.Infrastructure.Persistence.SqlServer.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(nameof(IDbContext.Cities), nameof(SupplierMaintenance));

        builder.HasOne(e => e.Province).WithMany(e => e.Cities).HasForeignKey(e => e.ProvinceId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(b => b.Name).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Name));
    }
}

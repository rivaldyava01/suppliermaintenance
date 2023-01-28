using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Domain.Entities;
using SupplierMaintenance.InfraStructure.Persistence.Common.Constants;
using SupplierMaintenance.Shared.Provinces.Constants;

namespace SupplierMaintenance.Infrastructure.Persistence.SqlServer.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable(nameof(IDbContext.Provinces), nameof(SupplierMaintenance));

        builder.Property(b => b.Name).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Name));
    }
}

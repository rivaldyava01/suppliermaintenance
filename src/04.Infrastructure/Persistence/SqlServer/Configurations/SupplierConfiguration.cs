using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Domain.Entities;
using SupplierMaintenance.InfraStructure.Persistence.Common.Constants;
using SupplierMaintenance.Shared.Suppliers.Constants;

namespace SupplierMaintenance.Infrastructure.Persistence.SqlServer.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable(nameof(IDbContext.Suppliers), nameof(SupplierMaintenance));

        builder.HasOne(e => e.Province).WithMany(e => e.Suppliers).HasForeignKey(e => e.ProvinceId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.City).WithMany(e => e.Suppliers).HasForeignKey(e => e.CityId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(b => b.SupplierCode).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.SupplierCode));
        builder.Property(b => b.SupplierName).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.SupplierName));
        builder.Property(b => b.Address).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Address));
        builder.Property(b => b.Pic).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Pic));
    }
}

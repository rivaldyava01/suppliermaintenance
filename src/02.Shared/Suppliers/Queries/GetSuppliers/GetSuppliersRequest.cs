using FluentValidation;
using SupplierMaintenance.Shared.Suppliers.Constants;

namespace SupplierMaintenance.Shared.Suppliers.Queries.GetSuppliers;

public class GetSuppliersRequest
{
    public string? SupplierCode { get; set; } = default!;
    public Guid? ProvinceId { get; set; }
    public Guid? CityId { get; set; }
}


namespace SupplierMaintenance.Shared.Suppliers.Queries.GetSuppliers;

public class GetSuppliersResponse
{
    public Guid Id { get; set; }
    public string SupplierCode { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string ProvinceName { get; set; } = default!;
    public string CityName { get; set; } = default!;
    public string Pic { get; set; } = default!;
}

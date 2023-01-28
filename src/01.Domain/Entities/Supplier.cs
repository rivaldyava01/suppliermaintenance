namespace SupplierMaintenance.Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }
    public string SupplierCode { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
    public string Address { get; set; } = default!;

    public Guid ProvinceId { get; set; } = default!;
    public Province Province { get; set; } = default!;

    public Guid CityId { get; set; } = default!;
    public City City { get; set; } = default!;

    public string Pic { get; set; } = default!;
}

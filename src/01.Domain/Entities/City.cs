namespace SupplierMaintenance.Domain.Entities;

public class City
{
    public Guid Id { get; set; }

    public Guid ProvinceId { get; set; } = default!;
    public Province Province { get; set; } = default!;

    public string Name { get; set; } = default!;

    public List<Supplier> Suppliers { get; set; } = new();
}

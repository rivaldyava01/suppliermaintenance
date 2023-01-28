namespace SupplierMaintenance.Domain.Entities;

public class Province
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public List<City> Cities { get; set; } = new();
    public List<Supplier> Suppliers { get; set; } = new();
}

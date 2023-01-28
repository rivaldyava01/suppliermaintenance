namespace SupplierMainTenance.Shared.Common.Responses;

public class PaginatedListResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalItems { get; set; }
}

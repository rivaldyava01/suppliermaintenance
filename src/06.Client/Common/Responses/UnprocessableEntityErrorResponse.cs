namespace SupplierMaintenance.Client.Common.Responses;

public class UnprocessableEntityErrorResponse : ErrorResponse
{
    public string Name { get; set; } = default!;
}

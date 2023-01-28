namespace SupplierMaintenance.Client.Common.Responses;

public class ResponseResult<T>
{
    public T? Result { get; set; }
    public ErrorResponse? Error { get; set; }
}

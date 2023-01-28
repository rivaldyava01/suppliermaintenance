using Microsoft.Extensions.Configuration;
using RestSharp;
using SupplierMaintenance.Client.Common.Extensions;
using SupplierMaintenance.Client.Common.Responses;
using SupplierMaintenance.Shared.Suppliers.Commands.AddSupplier;
using SupplierMaintenance.Shared.Suppliers.Commands.EditSupplier;
using SupplierMaintenance.Shared.Suppliers.Constants;
using SupplierMaintenance.Shared.Suppliers.Queries.GetSuppliers;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.Client.Services.BackEnd;

public class SupplierService
{
    private readonly RestClient _restClient;

    public SupplierService(IConfiguration configuration)
    {
        var backEndOptions = configuration.GetSection(BackEndOptions.SectionKey).Get<BackEndOptions>();

        _restClient = new RestClient(backEndOptions.BaseUrl);
    }

    public async Task<ResponseResult<ItemCreatedResponse>> AddSupplierAsync(AddSupplierRequest request)
    {
        var restRequest = new RestRequest(ApiEndpoint.Suppliers, Method.Post);

        restRequest.AddParameters(request);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<ItemCreatedResponse>();
    }

    public async Task<ResponseResult<NoContentResponse>> UpdateSupplierAsync(EditSupplierRequest request)
    {
        var restRequest = new RestRequest($"{ApiEndpoint.Suppliers}", Method.Put);

        restRequest.AddParameters(request);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<NoContentResponse>();
    }

    public async Task<ResponseResult<NoContentResponse>> DeleteSupplierAsync(Guid supplierId)
    {
        var restRequest = new RestRequest($"{ApiEndpoint.Suppliers}", Method.Delete);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<NoContentResponse>();
    }

    public async Task<ResponseResult<PaginatedListResponse<GetSuppliersResponse>>> GetSuppliersAsync(GetSuppliersRequest request)
    {
        var restRequest = new RestRequest(ApiEndpoint.Suppliers, Method.Get);
        restRequest.AddParameters(request);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<PaginatedListResponse<GetSuppliersResponse>>();
    }
}

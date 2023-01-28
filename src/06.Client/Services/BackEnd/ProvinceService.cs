using Microsoft.Extensions.Configuration;
using RestSharp;
using SupplierMaintenance.Client.Common.Extensions;
using SupplierMaintenance.Client.Common.Responses;
using SupplierMaintenance.Shared.Provinces.Queries.GetListProvinces;
using SupplierMaintenance.Shared.Provinces.Constants;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.Client.Services.BackEnd;

public class ProvinceService
{
    private readonly RestClient _restClient;

    public ProvinceService(IConfiguration configuration)
    {
        var backEndOptions = configuration.GetSection(BackEndOptions.SectionKey).Get<BackEndOptions>();

        _restClient = new RestClient(backEndOptions.BaseUrl);
    }

    public async Task<ResponseResult<ListResponse<GetListProvinces_Provinces>>> GetListProvincesAsync()
    {
        var restRequest = new RestRequest($"{ApiEndpoint.Provinces}/List", Method.Get);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<ListResponse<GetListProvinces_Provinces>>();
    }
}

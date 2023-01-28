using Microsoft.Extensions.Configuration;
using RestSharp;
using SupplierMaintenance.Client.Common.Extensions;
using SupplierMaintenance.Client.Common.Responses;
using SupplierMaintenance.Shared.Cities.Queries.GetListCities;
using SupplierMaintenance.Shared.Cities.Constants;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.Client.Services.BackEnd;

public class CityService
{
    private readonly RestClient _restClient;

    public CityService(IConfiguration configuration)
    {
        var backEndOptions = configuration.GetSection(BackEndOptions.SectionKey).Get<BackEndOptions>();

        _restClient = new RestClient(backEndOptions.BaseUrl);
    }

    public async Task<ResponseResult<ListResponse<GetListCities_Cities>>> GetListCitiesAsync()
    {
        var restRequest = new RestRequest($"{ApiEndpoint.Cities}/List", Method.Get);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        return restResponse.ToResponseResult<ListResponse<GetListCities_Cities>>();
    }
}

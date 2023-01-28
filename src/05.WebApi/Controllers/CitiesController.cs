using Microsoft.AspNetCore.Mvc;
using SupplierMaintenance.Shared.Cities.Queries.GetListCities;
using SupplierMainTenance.Cities.Queries.GetListCities;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.WebApi.Controllers;

[ApiController]
public class CitiesController : ApiControllerBase
{

    [HttpGet("List")]
    public async Task<ActionResult<ListResponse<GetListCities_Cities>>> GetListCities([FromQuery] GetListCitiesQuery request)
    {
        return await Mediator.Send(request);
    }
}

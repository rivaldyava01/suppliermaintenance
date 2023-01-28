using Microsoft.AspNetCore.Mvc;
using SupplierMaintenance.Shared.Provinces.Queries.GetListProvinces;
using SupplierMainTenance.Provinces.Queries.GetListProvinces;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.WebApi.Controllers;

[ApiController]
public class ProvincesController : ApiControllerBase
{
    [HttpGet("List")]
    public async Task<ActionResult<ListResponse<GetListProvinces_Provinces>>> GetListProvince()
    {
        return await Mediator.Send(new GetListProvincesQuery());
    }

}

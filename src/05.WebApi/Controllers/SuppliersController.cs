using Microsoft.AspNetCore.Mvc;
using SupplierMaintenance.Application.Provinces.Commands.GetSuppliers;
using SupplierMaintenance.Application.Suppliers.Commands.AddSupplier;
using SupplierMaintenance.Application.Suppliers.Commands.DeleteSupplier;
using SupplierMaintenance.Application.Suppliers.Commands.EditSupplier;
using SupplierMaintenance.Shared.Suppliers.Queries.GetSuppliers;
using SupplierMaintenance.WebApi.Common.Constants;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.WebApi.Controllers;

[ApiController]
public class SuppliersController : ApiControllerBase
{
    [HttpPost]
    [Consumes(RequestContentTypes.Form)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ItemCreatedResponse>> AddSupplier([FromForm] AddSupplierCommand request)
    {
        var response = await Mediator.Send(request);

        return CreatedAtAction(nameof(AddSupplier), new { id = response.Id }, response);
    }

    [HttpPut]
    [Consumes(RequestContentTypes.Form)]
    public async Task<ActionResult> UpdateSupplier([FromForm] EditSupplierCommand request)
    {
        await Mediator.Send(request);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteSupplier([FromForm] DeleteSupplierCommand request)
    {
        await Mediator.Send(request);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedListResponse<GetSuppliersResponse>>> GetSuppliers([FromQuery] GetSuppliersQuery request)
    {
        return await Mediator.Send(request);
    }

}

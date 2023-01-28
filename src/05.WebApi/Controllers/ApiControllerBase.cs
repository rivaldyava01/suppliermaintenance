using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupplierMaintenance.WebApi.Common.Filters.ApiException;

namespace SupplierMaintenance.WebApi.Controllers;

[ApiExceptionFilter]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private readonly ISender _mediator = default!;

    protected ISender Mediator
    {
        get
        {
            if (_mediator is not null)
            {
                return _mediator;
            }
            else
            {
                return HttpContext.RequestServices.GetRequiredService<ISender>();
            }
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Shared.Provinces.Queries.GetListProvinces;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMainTenance.Provinces.Queries.GetListProvinces;

public class GetListProvincesQuery : IRequest<ListResponse<GetListProvinces_Provinces>>
{
}

public class GetListProvincesQueryHandler : IRequestHandler<GetListProvincesQuery, ListResponse<GetListProvinces_Provinces>>
{
    private readonly IDbContext _context;

    public GetListProvincesQueryHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<ListResponse<GetListProvinces_Provinces>> Handle(GetListProvincesQuery request, CancellationToken cancellationToken)
    {

        var response = new ListResponse<GetListProvinces_Provinces>();

        var proviences = await _context.Provinces
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        foreach (var provience in proviences)
        {
            response.Items.Add(new GetListProvinces_Provinces
            {
                Id = provience.Id,
                Name = provience.Name
            });
        }

        return response;
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Application.Common.Exceptions;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Shared.Cities.Queries.GetListCities;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMainTenance.Cities.Queries.GetListCities;

public class GetListCitiesQuery : IRequest<ListResponse<GetListCities_Cities>>
{
    public Guid ProvinceId { get; set; }
}

public class GetListCitiesQueryHandler : IRequestHandler<GetListCitiesQuery, ListResponse<GetListCities_Cities>>
{
    private readonly IDbContext _context;

    public GetListCitiesQueryHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<ListResponse<GetListCities_Cities>> Handle(GetListCitiesQuery request, CancellationToken cancellationToken)
    {
        var response = new ListResponse<GetListCities_Cities>();

        var cities = await _context.Cities
            .AsNoTracking()
            .Where(x => x.ProvinceId == request.ProvinceId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        if(cities is null)
        {
            throw new NotFoundException();
        }

        foreach (var city in cities)
        {
            response.Items.Add(new GetListCities_Cities
            {
                Id = city.Id,
                Name = city.Name
            });
        }

        return response;
    }
}

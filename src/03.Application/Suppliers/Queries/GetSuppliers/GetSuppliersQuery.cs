using MediatR;
using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Application.Common.Exceptions;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Shared.Suppliers.Queries.GetSuppliers;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.Application.Provinces.Commands.GetSuppliers;

public class GetSuppliersQuery : GetSuppliersRequest, IRequest<PaginatedListResponse<GetSuppliersResponse>>
{
}

public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, PaginatedListResponse<GetSuppliersResponse>>
{
    private readonly IDbContext _context;

    public GetSuppliersQueryHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedListResponse<GetSuppliersResponse>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
    {
        var response = new PaginatedListResponse<GetSuppliersResponse>();
        var query = _context.Suppliers.AsNoTracking();

        if (request.SupplierCode != null || request.ProvinceId != null || request.CityId != null){
            query = query
                 .Where(x => x.SupplierCode == request.SupplierCode || x.ProvinceId == request.ProvinceId || x.CityId == request.CityId)
                 .OrderBy(x => x.SupplierCode);
        }

        var suppliers = await query.ToListAsync(cancellationToken);

        foreach (var supplier in suppliers)
        {
            var province = await _context.Provinces
            .AsNoTracking()
            .Where(x => x.Id == supplier.ProvinceId)
            .SingleOrDefaultAsync(cancellationToken);

            if(province == null)
            {
                throw new NotFoundException();
            }

            var city = await _context.Cities
            .AsNoTracking()
            .Where(x => x.Id == supplier.CityId)
            .SingleOrDefaultAsync(cancellationToken);

            if (city == null)
            {
                throw new NotFoundException();
            }

            response.Items.Add(new GetSuppliersResponse
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                SupplierCode = supplier.SupplierCode,
                Address = supplier.Address,
                ProvinceName = province.Name,
                CityName =  city.Name,
                Pic = supplier.Pic,
            });
        }

        return response;
    }
}

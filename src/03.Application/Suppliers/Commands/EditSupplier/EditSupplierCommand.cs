using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Application.Common.Exceptions;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Shared.Suppliers.Commands.EditSupplier;

namespace SupplierMaintenance.Application.Suppliers.Commands.EditSupplier;

public class EditSupplierCommand : EditSupplierRequest, IRequest
{
}

public class EditSupplierCommandValidator : AbstractValidator<EditSupplierCommand>
{
    public EditSupplierCommandValidator()
    {
        Include(new EditSupplierRequestValidator());
    }
}

public class EditSupplierCommandHandler : IRequestHandler<EditSupplierCommand>
{
    private readonly IDbContext _context;

    public EditSupplierCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(EditSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _context.Suppliers
            .Where(x => x.SupplierCode == request.SupplierCode)
            .SingleOrDefaultAsync(cancellationToken);

        if (supplier is null)
        {
            throw new NotFoundException();
        }

            supplier.SupplierName = request.SupplierName;
            supplier.SupplierCode = request.SupplierCode;
            supplier.Address = request.Address;
            supplier.ProvinceId = request.ProvinceId;
            supplier.CityId = request.CityId;
            supplier.Pic = request.Pic;
       

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

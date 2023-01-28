using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Application.Common.Exceptions;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Domain.Entities;
using SupplierMaintenance.Shared.Suppliers.Commands.AddSupplier;
using SupplierMaintenance.Shared.Suppliers.Constants;
using SupplierMainTenance.Shared.Common.Responses;

namespace SupplierMaintenance.Application.Suppliers.Commands.AddSupplier;

public class AddSupplierCommand : AddSupplierRequest, IRequest<ItemCreatedResponse>
{
}

public class AddSupplierCommandValidator : AbstractValidator<AddSupplierCommand>
{
    public AddSupplierCommandValidator()
    {
        Include(new AddSupplierRequestValidator());
    }
}

public class AddSupplierCommandHandler : IRequestHandler<AddSupplierCommand, ItemCreatedResponse>
{
    private readonly IDbContext _context;

    public AddSupplierCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<ItemCreatedResponse> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplierWithTheSameCode = await _context.Suppliers
            .Where(x => x.SupplierCode == request.SupplierCode)
            .SingleOrDefaultAsync(cancellationToken);

        if (supplierWithTheSameCode is not null)
        {
            throw new AlreadyExistsException(DisplayTextFor.Supplier, DisplayTextFor.SupplierCode, request.SupplierCode);
        }

        var supplier = new Supplier
        {
            Id = Guid.NewGuid(),
            SupplierName = request.SupplierName,
            SupplierCode = request.SupplierCode,
            Address = request.Address,
            ProvinceId = request.ProvinceId,
            CityId = request.CityId,
            Pic = request.Pic
        };

        await _context.Suppliers.AddAsync(supplier, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ItemCreatedResponse
        {
            Id = supplier.Id
        };
    }
}

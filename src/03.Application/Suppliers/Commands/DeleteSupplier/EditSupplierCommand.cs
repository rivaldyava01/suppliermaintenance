using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SupplierMaintenance.Application.Common.Exceptions;
using SupplierMaintenance.Application.Services.Persistence;
using SupplierMaintenance.Shared.Suppliers.Commands.DeleteSupplier;

namespace SupplierMaintenance.Application.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierCommand : DeleteSupplierRequest, IRequest
{
}

public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
{
    public DeleteSupplierCommandValidator()
    {
        Include(new DeleteSupplierRequestValidator());
    }
}

public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
{
    private readonly IDbContext _context;

    public DeleteSupplierCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _context.Suppliers
            .Where(x => x.SupplierCode == request.SupplierCode)
            .SingleOrDefaultAsync(cancellationToken);

        if (supplier is null)
        {
            throw new NotFoundException();
        }

        _context.Suppliers.RemoveRange(supplier);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

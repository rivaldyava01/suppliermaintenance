using FluentValidation;
using SupplierMaintenance.Shared.Suppliers.Constants;

namespace SupplierMaintenance.Shared.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierRequest
{
    public string SupplierCode { get; set; } = default!;
}

public class DeleteSupplierRequestValidator : AbstractValidator<DeleteSupplierRequest>
{
    public DeleteSupplierRequestValidator()
    {

        RuleFor(x => x.SupplierCode)
            .NotEmpty();
    }
}

using FluentValidation;
using SupplierMaintenance.Shared.Suppliers.Constants;

namespace SupplierMaintenance.Shared.Suppliers.Commands.EditSupplier;

public class EditSupplierRequest
{
    public string SupplierCode { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public Guid ProvinceId { get; set; }
    public Guid CityId { get; set; }
    public string Pic { get; set; } = default!;
}

public class EditSupplierRequestValidator : AbstractValidator<EditSupplierRequest>
{
    public EditSupplierRequestValidator()
    {
        RuleFor(x => x.SupplierName)
            .NotEmpty()
            .MinimumLength(MinimumLengthFor.SupplierName)
            .MaximumLength(MaximumLengthFor.SupplierName);

        RuleFor(x => x.SupplierCode)
            .NotEmpty();

        RuleFor(x => x.Pic)
            .NotEmpty()
            .MinimumLength(MinimumLengthFor.Pic)
            .MaximumLength(MaximumLengthFor.Pic);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MinimumLength(MinimumLengthFor.Address)
            .MaximumLength(MaximumLengthFor.Address);

        RuleFor(x => x.ProvinceId)
            .NotEmpty();

        RuleFor(x => x.CityId)
            .NotEmpty();

    }
}

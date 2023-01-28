using SupplierMaintenance.Shared.Common.Constants;

namespace SupplierMaintenance.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base($"The data is not found.")
    {
    }

    public NotFoundException(string entityName, object id)
        : base($"The {entityName} with {CommonDisplayTextFor.Id} [{id}] is not found.")
    {
    }

    public NotFoundException(string entityName, string fieldName, object fieldValue)
        : base($"The {entityName} with {fieldName} [{fieldValue}] is not found.")
    {
    }
}

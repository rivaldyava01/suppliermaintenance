namespace SupplierMaintenance.Application.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string entityName, string entityField, object value)
        : base($"The {entityName} with {entityField} [{value}] already exists.")
    {
    }
}

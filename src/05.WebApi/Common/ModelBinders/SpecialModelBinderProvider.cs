using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SupplierMaintenance.Shared.Common.Attributes;
using SupplierMaintenance.WebApi.Common.ModelBinders.Json;

namespace SupplierMaintenance.WebApi.Common.ModelBinders;

public class SpecialModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var propertyName = context.Metadata.PropertyName;

        if (propertyName is null)
        {
            return null;
        }

        var propertyInfo = context.Metadata.ContainerType?.GetProperty(propertyName);

        if (propertyInfo is null)
        {
            return null;
        }

        var attribute = propertyInfo.GetCustomAttribute<JsonValueAttribute>();

        if (attribute is not null)
        {
            return new JsonModelBinder();
        }
        else
        {
            return null;
        }
    }
}

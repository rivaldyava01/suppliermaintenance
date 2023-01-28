using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace SupplierMaintenance.WebApi.Common.ModelBinders.Json;

public class JsonModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        var theValue = valueProviderResult.FirstValue;

        if (theValue is null)
        {
            return Task.CompletedTask;
        }

        try
        {
            var result = JsonConvert.DeserializeObject(theValue, bindingContext.ModelType);

            if (result is not null)
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
        }
        catch (JsonReaderException exception)
        {
            var modelBindingKey = bindingContext.IsTopLevelObject ? bindingContext.BinderModelName ?? string.Empty : bindingContext.ModelName;

            bindingContext.ModelState.TryAddModelException(modelBindingKey, exception);

            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }

        return Task.CompletedTask;
    }
}

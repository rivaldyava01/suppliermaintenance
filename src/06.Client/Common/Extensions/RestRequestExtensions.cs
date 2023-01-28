using System.Collections;
using Newtonsoft.Json;
using RestSharp;
using SupplierMaintenance.Shared.Common.Attributes;

namespace SupplierMaintenance.Client.Common.Extensions;

public static class RestRequestExtensions
{
    private const string UniversalDateTimeFormat = "yyyy/MM/dd HH:mm:ss";

    public static void AddParameters<T>(this RestRequest restRequest, T request) where T : notnull
    {
        foreach (var property in typeof(T).GetProperties())
        {
            var theValue = property.GetValue(request);

            if (theValue is null)
            {
                continue;
            }

            var hasJsonAttribute = property.GetCustomAttributes(typeof(JsonValueAttribute), false).Any();

            if (hasJsonAttribute)
            {
                var value = JsonConvert.SerializeObject(theValue);

                restRequest.AddParameter(property.Name, value);
            }
            else if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                var index = 0;

                foreach (var childItem in ((IEnumerable)theValue).OfType<object>())
                {
                    foreach (var childItemProperty in childItem.GetType().GetProperties())
                    {
                        var name = $"{property.Name}[{index}].{childItemProperty.Name}";
                        var value = childItemProperty.GetValue(childItem)?.ToString();

                        restRequest.AddParameter(name, value);
                    }

                    index++;
                }
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                var dateTime = (DateTime)theValue;

                restRequest.AddQueryParameter(property.Name, dateTime.ToString(UniversalDateTimeFormat));
            }
            else if (property.PropertyType == typeof(DateTime?))
            {
                var nullableDateTime = (DateTime?)theValue;

                restRequest.AddQueryParameter(property.Name, nullableDateTime.Value.ToString(UniversalDateTimeFormat));
            }
            else
            {
                restRequest.AddParameter(property.Name, theValue.ToString());
            }
        }
    }
}

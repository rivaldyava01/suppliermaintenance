using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SupplierMaintenance.Application.Common.Extensions;

public static class GenericExtensions
{
    public static string ToPrettyJson<TRequest>(this TRequest request)
    {
        return JToken.Parse(JsonConvert.SerializeObject(request)).ToString(Formatting.Indented);
    }
}

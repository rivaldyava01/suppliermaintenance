using System.Net;
using Newtonsoft.Json;
using RestSharp;
using SupplierMaintenance.Client.Common.Responses;

namespace SupplierMaintenance.Client.Common.Extensions;

public static class RestResponseExtensions
{
    public static ResponseResult<T> ToResponseResult<T>(this RestResponse restResponse)
    {
        var responseResult = new ResponseResult<T>();

        try
        {
            if (restResponse.IsSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(restResponse.Content))
                {
                    var response = JsonConvert.DeserializeObject<T>(restResponse.Content);

                    if (response is null)
                    {
                        throw new Exception($"Failed to deserialize JSON content {restResponse.Content} into {typeof(T).Name}.");
                    }

                    responseResult.Result = response;
                }
                else
                {
                    var noContent = JsonConvert.SerializeObject(new NoContentResponse());

                    responseResult.Result = JsonConvert.DeserializeObject<T>(noContent);
                }
            }
            else if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                responseResult.Error = new ErrorResponse
                {
                    Detail = "Kagak authorized cuy"
                };
            }
            else if (restResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                responseResult.Error = new ErrorResponse
                {
                    Detail = "Anda gak boleh akses API tersebut"
                };
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(restResponse.Content))
                {
                    responseResult.Error = JsonConvert.DeserializeObject<ErrorResponse>(restResponse.Content);
                }
                else
                {
                    responseResult.Error = new ErrorResponse
                    {
                        Detail = "Ngapa nih yakk"
                    };
                }
            }
        }
        catch (Exception exception)
        {
            responseResult.Error = new ErrorResponse
            {
                Type = "Unknown Error",
                Title = $"{exception.GetType().FullName}: {exception.Message}",
                Status = restResponse.StatusCode,
                Detail = $"{restResponse.Content} [{exception.GetType().FullName}: {exception.Message}] {exception.StackTrace}",
            };
        }

        return responseResult;
    }
}

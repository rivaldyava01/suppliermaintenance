using MediatR;
using Microsoft.Extensions.Logging;
using SupplierMaintenance.Application.Common.Extensions;

namespace SupplierMaintenance.Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            var requestName = typeof(TRequest).Name;
            var formattedRequest = request.ToPrettyJson();

            _logger.LogError(exception, "Unhandled Exception when executing {RequestName}.\n{RequestName}\n{RequestObject}",
               requestName, requestName, formattedRequest);

            throw;
        }
    }
}

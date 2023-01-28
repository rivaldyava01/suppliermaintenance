using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using SupplierMaintenance.Application.Common.Extensions;

namespace SupplierMaintenance.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehavior(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 1000)
        {
            var requestName = typeof(TRequest).Name;
            var formattedRequest = request.ToPrettyJson();

            if (elapsedMilliseconds > 5000)
            {
                _logger.LogError("{RequestName} for ({ElapsedMilliseconds} milliseconds).\n{RequestName}\n{RequestObject}",
                   requestName, elapsedMilliseconds, requestName, formattedRequest);
            }
            else
            {
                _logger.LogWarning("{RequestName} for ({ElapsedMilliseconds} milliseconds).\n{RequestName}\n{RequestObject}",
                   requestName, elapsedMilliseconds, requestName, formattedRequest);
            }
        }

        return response;
    }
}

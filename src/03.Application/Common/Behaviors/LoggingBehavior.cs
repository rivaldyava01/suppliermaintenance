using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SupplierMaintenance.Application.Common.Extensions;

namespace SupplierMaintenance.Application.Common.Behaviors;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var formattedRequestObject = request.ToPrettyJson();

        _logger.LogInformation("Processing {RequestName}.\n{RequestObject}", requestName, formattedRequestObject);

        return Task.CompletedTask;
    }
}

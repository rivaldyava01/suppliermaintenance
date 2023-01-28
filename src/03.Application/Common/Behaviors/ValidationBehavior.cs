using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SupplierMaintenance.Application.Common.Exceptions;

namespace SupplierMaintenance.Application.Common.Behaviors;

public class ValidationBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(ILogger<TRequest> logger, IEnumerable<IValidator<TRequest>> validators)
    {
        _logger = logger;
        _validators = validators;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(failure => failure is not null).ToList();

            if (failures.Any())
            {
                var requestName = typeof(TRequest).Name;
                var exception = new ApplicationValidationException(failures);

                var formattedSummary = new
                {
                    Summary = exception.Summary,
                };

                _logger.LogError("Validation failed for {RequestName}.\n{FormattedSummary}", requestName, formattedSummary);

                throw exception;
            }
        }
    }
}

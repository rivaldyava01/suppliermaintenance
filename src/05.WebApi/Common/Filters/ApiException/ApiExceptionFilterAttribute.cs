using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SupplierMaintenance.Application.Common.Exceptions;

namespace SupplierMaintenance.WebApi.Common.Filters.ApiException;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is ApplicationValidationException applicationValidationException)
        {
            var details = new ValidationProblemDetails(applicationValidationException.Errors)
            {
                Type = ProblemDetailsFor.ValidationException.Type
            };

            context.Result = new UnprocessableEntityObjectResult(details);
        }
        else if (exception is MismatchException)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Type = ProblemDetailsFor.MismatchException.Type,
                Title = ProblemDetailsFor.MismatchException.Title,
                Detail = exception.Message
            };

            context.Result = new UnprocessableEntityObjectResult(details);
        }
        else if (exception is AlreadyExistsException)
        {
            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Type = ProblemDetailsFor.AlreadyExistsExceptions.Type,
                Title = ProblemDetailsFor.AlreadyExistsExceptions.Title,
                Detail = exception.Message
            };

            context.Result = new ConflictObjectResult(details);
        }
        else if (exception is NotFoundException)
        {
            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = ProblemDetailsFor.NotFoundException.Type,
                Title = ProblemDetailsFor.NotFoundException.Title,
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);
        }
        else if (exception is InvalidOperationException)
        {
            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Type = ProblemDetailsFor.InvalidOperationException.Type,
                Title = ProblemDetailsFor.InvalidOperationException.Title,
                Detail = exception.Message
            };

            context.Result = new UnprocessableEntityObjectResult(details);
        }
        else if (context.Exception is ArgumentException)
        {
            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Type = ProblemDetailsFor.ArgumentException.Type,
                Title = ProblemDetailsFor.ArgumentException.Title,
                Detail = context.Exception.Message
            };

            context.Result = new UnprocessableEntityObjectResult(details);
        }
        else
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = ProblemDetailsFor.UnknownException.Type,
                Title = ProblemDetailsFor.UnknownException.Title,
                Detail = exception.Message
            };

            context.Result = new ObjectResult(details);
        }

        context.ExceptionHandled = true;
    }
}

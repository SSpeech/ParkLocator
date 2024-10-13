
using Microsoft.AspNetCore.Mvc;
using ParkLocator.Shared.Errors;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Create.Extensions
{
    public static class ResponseExtensions
    {
        public static ProblemDetails ResponseExtension(this Result<Guid> results)
        {
            var (statusCode, type) = DetermineErrorStatus(results.Errors);
            return new()
            {
                Title = results.Error.Code,
                Type = type,
                Detail = results.Error.Message,
                Status = statusCode,
                Extensions = { { nameof(results.Errors), results.Errors } }
            };
        }
        private static (int, string) DetermineErrorStatus(List<Error> errors) => errors.Count switch
        {
            0 => (StatusCodes.Status409Conflict, ErrorType.Conflict.Type),
            _ => (StatusCodes.Status400BadRequest, ErrorType.Conflict.Type)
        };
    }
}

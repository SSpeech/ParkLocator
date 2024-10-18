using Microsoft.AspNetCore.Mvc;
using ParkLocator.Shared.Errors;

namespace ParkLocator.Features.Regions.Queries
{
    //TODO: This class was located with the Commands and it's a Query Response Extension
    //I think it's way too generic to have it here with the Regions...
    public static class ResponseExtensions
    {
        public static ProblemDetails ResponseExtension(this List<Error> errors, Error error)
        {
            var (statusCode, type) = DetermineErrorStatus(errors);
            return new()
            {
                Title = error.Code,
                Type = type,
                Detail = error.Message,
                Status = statusCode,
                Extensions = { { nameof(errors), errors } }
            };
        }
        private static (int, string) DetermineErrorStatus(List<Error> errors) => errors.Count switch
        {
            0 => (StatusCodes.Status409Conflict, ErrorType.Conflict.Type),
            _ => (StatusCodes.Status400BadRequest, ErrorType.Conflict.Type)
        };
    }
}

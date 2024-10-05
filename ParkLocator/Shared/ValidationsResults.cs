using FluentValidation.Results;
using ParkLocator.Shared.Errors;
using ParkLocator.Shared.Results;

namespace ParkLocator.Shared
{
    public static class ValidationsResults<TValue>
    {
        public static Result<TValue> CreateErrorResult(List<Error> errors, string code, string message)
        {
            return Result.Failure<TValue>(errors,
                new Error(code,
                message));
        }

        public static List<Error> ValidationResultErrors(ValidationResult validationResult)
        {
            return validationResult.Errors.Where(IsValidationFailure)
                  .Select(validationFailure => new Error(
                     validationFailure.PropertyName,
                     validationFailure.ErrorMessage)
                  ).Distinct()
                  .ToList();
        }

        public static bool IsValidationFailure(ValidationFailure validationFailure)
        {
            return validationFailure is not null;
        }
    }
}

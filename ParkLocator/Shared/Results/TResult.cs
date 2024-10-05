using ParkLocator.Shared.Errors;

namespace ParkLocator.Shared.Results;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, List<Error> errors, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
        Errors = errors;
    }
    public List<Error> Errors { get; }

    public TValue? Value => _value;
    public static implicit operator Result<TValue>(TValue? value) => Create(value!);
}

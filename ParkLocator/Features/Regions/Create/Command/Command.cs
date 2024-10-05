using MediatR;
using ParkLocator.Shared.Results;

namespace ParkLocator.Features.Regions.Create.Command;

public class Command : IRequest<Result<Guid>>
{
    public Command(string country, string name, string stateProvince, string locality)
    {
        Country = country;
        Name = name;
        StateProvince = stateProvince;
        Locality = locality;
    }
    public string Country { get; private set; }
    public string Name { get; private set; }
    public string StateProvince { get; }
    public string Locality { get; private set; }
}


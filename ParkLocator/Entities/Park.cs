using Throw;

namespace ParkLocator.Entities;

public class Park : BaseEntity
{
    public string Name { get; private init; }
    public string Description { get; private init; }
    public Guid DistrictId { get; }
    public virtual District District { get; private set; }
    protected Park() { }
    public Park(string name, string description, District district)
    {
        Validate(name, description);
        Name = name;
        Description = description;
        District = district;
    }
    private static void ValidatesDescription(string description) =>
        description.Throw()
        .IfEmpty()
        .IfLongerThan(500)
        .IfShorterThan(2);
    private static void ValidateName(string name) =>
        name.Throw()
        .IfEmpty()
        .IfLongerThan(50)
        .IfShorterThan(2);
    private static void Validate(string name, string description)
    {
        ValidateName(name);
        ValidatesDescription(description);
    }
}

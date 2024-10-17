namespace ParkLocator.Entities;

public class Region : BaseEntity
{
    public string Country { get; private set; }
    public string Name { get; private init; }
    public string StateProvince { get; private init; }
    public ICollection<District> Districts { get; private set; }
    public string Locality { get; private init; }
    protected Region() { }
    public Region(ICollection<District> districts, string country, string name, string stateProvince, string locality)
    {
        Districts = districts;
        Country = country;
        Name = name;
        StateProvince = stateProvince;
        Locality = locality;
    }
}

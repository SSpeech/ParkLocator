namespace ParkLocator.Entities;

public class Region : BaseEntity
{
    protected Region() { }

    public Region(string country, string name, string stateProvince, string locality)
    {
        Country = country;
        Name = name;
        StateProvince = stateProvince;
        Locality = locality;
    }

    public string Country { get; private set; }
    public string Name { get; private init; }
    public string StateProvince { get; private init; }
    public string Locality { get; private init; }

    public ICollection<District> Districts { get; private set; }
}
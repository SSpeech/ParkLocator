namespace ParkLocator.Entities;

public class Street : BaseEntity
{
    protected Street() { }

    public Street(string postalCode, string name, double latitude, double longitude, District district)
    {
        PostalCode = postalCode;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        District = district;
    }

    public string PostalCode { get; private init; }
    public string Name { get; private init; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public Guid DistrictId { get; private set; }
    public virtual District District { get; private set; }
}

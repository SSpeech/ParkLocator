using ParkLocator.Entities.Districts;

namespace ParkLocator.Entities.Streets;

public class Street
{
    public string PostalCode { get; private init; }
    public string Name { get; private init; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public Guid Id { get; }
    public Guid DistrictId { get; private set; }
    public District District { get; private set; }

    private Street(string postalCode, string name, double latitude, double longitude, Guid districtId, District district)
    {
        PostalCode = postalCode;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        Id = Guid.NewGuid();
        DistrictId = districtId;
        District = district;
    }
    private Street() { }
    public sealed class Builder
    {

        private string _postalCode = string.Empty;
        private string _name = string.Empty;
        private double _latitude = default;
        private double _longitude = default;
        private Guid _districtId = Guid.Empty;
        private District? _district;
        public Builder WithDistrictId(Guid districtId)
        {
            _districtId = districtId;
            return this;
        }
        public Builder WithDistrict(District district)
        {
            _district = district;
            return this;
        }
        public Builder WithPostalCode(string postalCode)
        {
            _postalCode = postalCode;
            return this;
        }
        public Builder WithStreetAddress(string name)
        {
            _name = name;
            return this;
        }
        public Builder WithLatitude(double latitude)
        {
            _latitude = latitude;
            return this;
        }
        public Builder WithLongitude(double longitude)
        {
            _longitude = longitude;
            return this;
        }

        public Street Build()
        {
            return new(_postalCode, _name, _latitude, _longitude, _districtId, _district);
        }
    }
}

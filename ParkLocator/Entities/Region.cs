namespace ParkLocator.Entities;

public class Region
{
    public string Country { get; private set; }
    public string Name { get; private init; }
    public string StateProvince { get; private init; }
    public ICollection<District> Districts { get; private set; }
    public Guid Id { get; }
    public string Locality { get; private init; }
    private Region() { }
    private Region(ICollection<District> districts, string country, string name, string stateProvince, string locality)
    {
        Districts = districts;
        Country = country;
        Name = name;
        StateProvince = stateProvince;
        Locality = locality;
    }
    public sealed class Builder
    {
        private ICollection<District> _districts = [];
        private string _country = string.Empty;
        private string _name = string.Empty;
        private string _stateProvince = string.Empty;
        private string _locality = string.Empty;
        public Builder WithDistrict(ICollection<District> districts)
        {
            _districts = districts;
            return this;
        }
        public Builder WithCountry(string country)
        {
            _country = country;
            return this;
        }
        public Builder WithName(string name)
        {
            _name = name;
            return this;
        }
        public Builder WithSateProvince(string province)
        {
            _stateProvince = province;
            return this;
        }
        public Builder WithLocality(string locality)
        {
            _locality = locality;
            return this;
        }
        public Region Build()
        {
            return new(_districts!, _country, _name, _stateProvince, _locality);
        }
    }
}

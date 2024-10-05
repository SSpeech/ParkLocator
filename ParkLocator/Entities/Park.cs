using Throw;

namespace ParkLocator.Entities;

public sealed class Park
{
    public Guid Id { get; }
    public string Name { get; private init; }
    public string Description { get; private init; }
    public Guid DistrictId { get; private set; }
    public District District { get; private set; }
    private Park(string name, string description, Guid districtId, District district)
    {
        Validate(name, description);
        Name = name;
        Description = description;
        DistrictId = districtId;
        District = district;
    }
    private Park()
    {

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

    public sealed class Builder
    {
        private string _name = string.Empty;
        private Guid _districtId = Guid.Empty;
        private District _district;
        private string _description = string.Empty;
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
        public Builder WithName(string name)
        {
            _name = name;
            return this;
        }
        public Builder WithDescription(string description)
        {
            _description = description;
            return this;
        }
        public Park Build()
        {
            return new(_name, _description, _districtId, _district);
        }
    }

}

namespace ParkLocator.Entities;

public class District
{
    public Guid Id { get; }
    public Guid RegionId { get; private init; }
    public Region Region { get; private init; }
    public HashSet<Park> Parks { get; private set; }
    public ICollection<Street> Streets { get; private set; }
    private District() { }

    private District(Region region, Guid regionId, HashSet<Park> parks, ICollection<Street> streets)
    {
        Region = region;
        RegionId = regionId;
        Parks = parks;
        Streets = streets;
    }
    public sealed class Builder
    {
        private Region _region;
        private HashSet<Park> _parks = [];
        private ICollection<Street> _streets = [];
        private Guid _regionId;

        public Builder WithParks(HashSet<Park> parks)
        {
            _parks = parks;
            return this;
        }
        public Builder WithStreets(ICollection<Street> streets)
        {
            _streets = streets;
            return this;
        }
        public Builder WithRegionId(Guid regionId)
        {
            _regionId = regionId;
            return this;
        }
        public Builder WithRegion(Region region)
        {
            _region = region;
            return this;
        }
        public District Build()
        {
            return new(_region, _regionId, _parks, _streets);
        }
    }
}

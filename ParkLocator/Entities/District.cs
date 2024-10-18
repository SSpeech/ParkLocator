namespace ParkLocator.Entities;

public class District : BaseEntity
{
    protected District() { }

    public District(Region region, HashSet<Park> parks, ICollection<Street> streets)
    {
        Region = region;
        Parks = parks;
        Streets = streets;
    }

    public Guid RegionId { get; private set; }
    public virtual Region Region { get; private init; }

    public HashSet<Park> Parks { get; private set; }

    public ICollection<Street> Streets { get; private set; }
}

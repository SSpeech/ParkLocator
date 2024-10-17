namespace ParkLocator.Entities;

public class District : BaseEntity
{
    public Guid RegionId { get; }
    public virtual Region Region { get; private init; }
    public HashSet<Park> Parks { get; private set; }
    public ICollection<Street> Streets { get; private set; }
    protected District() { }

    public District(Region region, HashSet<Park> parks, ICollection<Street> streets)
    {
        Region = region;
        Parks = parks;
        Streets = streets;
    }
}

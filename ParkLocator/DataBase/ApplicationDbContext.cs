using Microsoft.EntityFrameworkCore;
using ParkLocator.DataBase.Configurations;
using ParkLocator.Entities.Districts;
using ParkLocator.Entities.Parks;
using ParkLocator.Entities.Regions;
using ParkLocator.Entities.Streets;

namespace ParkLocator.DataBase;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Park> Parks { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Street> Streets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RegionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ParkEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DistrictEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StreetEntityConfiguration());
    }
}
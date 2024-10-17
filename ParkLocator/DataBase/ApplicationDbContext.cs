using Microsoft.EntityFrameworkCore;
using ParkLocator.Entities;
using System.Reflection;

namespace ParkLocator.DataBase;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Park> Parks { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Street> Streets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
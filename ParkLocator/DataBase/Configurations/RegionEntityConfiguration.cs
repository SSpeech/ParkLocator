using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkLocator.Entities;
using ParkLocator.Shared;

namespace ParkLocator.DataBase.Configurations;

public class RegionEntityConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable(nameof(Region));

        //TODO: Have a look at this extension method. Remember: DRY
        builder.ConfigureKeyValueOnAdd();

        builder.Property(region => region.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasMany(region => region.Districts)
            .WithOne(district => district.Region)
            .HasForeignKey(district => district.RegionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(region => region.Name);
        builder.Property(region => region.Country)
                .IsRequired().HasMaxLength(256);

        builder.Property(region => region.StateProvince)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(region => region.Locality)
            .IsRequired()
            .HasMaxLength(256);
    }
}

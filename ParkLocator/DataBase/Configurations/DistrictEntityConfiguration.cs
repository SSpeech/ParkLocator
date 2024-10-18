using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkLocator.Entities;
using ParkLocator.Shared;

namespace ParkLocator.DataBase.Configurations;

public class DistrictEntityConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable(nameof(District));

        builder.ConfigureKeyValueOnAdd();

        builder.HasMany(district => district.Parks)
            .WithOne(park => park.District)
            .HasForeignKey(park => park.DistrictId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(district => district.Streets)
            .WithOne(street => street.District)
            .HasForeignKey(street => street.DistrictId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(district => district.Region)
             .WithMany(region => region.Districts)
             .HasForeignKey(district => district.RegionId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
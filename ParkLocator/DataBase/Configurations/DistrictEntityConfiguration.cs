using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkLocator.Entities.Districts;

namespace ParkLocator.DataBase.Configurations;

public class DistrictEntityConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable(nameof(District));
        builder.Property(b => b.Id)
       .IsRequired()
       .ValueGeneratedOnAdd();

        builder.HasKey(district => district.Id);

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
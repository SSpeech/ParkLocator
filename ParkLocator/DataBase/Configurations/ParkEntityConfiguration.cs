using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkLocator.Entities;
using ParkLocator.Shared;

namespace ParkLocator.DataBase.Configurations;

public class ParkEntityConfiguration : IEntityTypeConfiguration<Park>
{
    public void Configure(EntityTypeBuilder<Park> builder)
    {
        builder.ToTable(nameof(Park));

        builder.ConfigureKeyValueOnAdd();

        builder.Property(park => park.Name)
                .IsRequired()
                .HasMaxLength(256);

        builder.Property(park => park.Description)
                .IsRequired()
                .HasMaxLength(500);

        builder.HasOne(park => park.District)
            .WithMany(district => district.Parks)
            .HasForeignKey(park => park.DistrictId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(park => park.Name);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkLocator.Entities;
using ParkLocator.Shared;

namespace ParkLocator.DataBase.Configurations;

public class StreetEntityConfiguration : IEntityTypeConfiguration<Street>
{
    public void Configure(EntityTypeBuilder<Street> builder)
    {
        builder.ToTable(nameof(Street));

        builder.ConfigureKeyValueOnAdd();

        builder.Property(street => street.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(street => street.PostalCode)
                .IsRequired()
                .HasMaxLength(10);
        builder.Property(street => street.Latitude)
                .IsRequired()
                .HasMaxLength(25);
        builder.Property(street => street.Longitude)
                .IsRequired()
                .HasMaxLength(25);

        builder.HasOne(street => street.District)
            .WithMany(district => district.Streets)
            .HasForeignKey(park => park.DistrictId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(park => park.Name);

    }
}

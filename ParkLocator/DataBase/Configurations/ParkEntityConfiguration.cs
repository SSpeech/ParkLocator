using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkLocator.Entities;

namespace ParkLocator.DataBase.Configurations;

public class ParkEntityConfiguration : IEntityTypeConfiguration<Park>
{
    public void Configure(EntityTypeBuilder<Park> builder)
    {
        builder.ToTable(nameof(Park));
        builder.Property(b => b.Id)
       .IsRequired()
       .ValueGeneratedOnAdd();
        builder.HasKey(park => park.Id);

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

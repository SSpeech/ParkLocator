using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkLocator.DataBase;
using ParkLocator.Entities;

namespace ParkLocator.Shared;

public static class Extensions
{
    /// <summary>
    /// Sets the <see cref="BaseEntity"/>'s key value to be generated on add
    /// along with the key being required.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static PropertyBuilder<Guid> ConfigureKeyValueOnAdd<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
    {
        builder.HasKey(e => e.Id);

        return builder.Property(b => b.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();
    }

    /// <summary>
    /// Checks if the Entity with the given id exists in the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <param name="dbContext"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<bool> Exists<T>(this Guid id, ApplicationDbContext dbContext, CancellationToken cancellationToken) where T : BaseEntity =>
        dbContext.Set<T>()
                    .AsNoTracking()
                    .AnyAsync(x => x.Id == id, cancellationToken);
}
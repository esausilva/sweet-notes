using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions;

public static class UserModelBuilderExtensions
{
    public static ModelBuilder ConfigureUserModel(this ModelBuilder builder)
    {
        builder
            .Entity<User>()
            .HasKey(x => x.Id);

        builder
            .Entity<User>()
            .HasIndex(x => x.EmailAddress, "IX_User_Unique_EmailAddress")
            .IsUnique();

        builder
            .Entity<User>()
            .Property(x => x.SignedUpUTC)
            .HasDefaultValueSql("(now() at time zone 'utc')")
            .ValueGeneratedOnAdd();

        builder
            .Entity<User>()
            .HasMany(x => x.Notes)
            .WithOne(x => x.User)
            .IsRequired();

        builder
            .Entity<User>()
            .HasMany(x => x.SpecialSomeones)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
        
        return builder;
    }
}
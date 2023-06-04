using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class UserModelConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .HasIndex(x => x.EmailAddress, "IX_User_Unique_EmailAddress")
            .IsUnique();

        builder
            .Property(x => x.SignedUpUTC)
            .HasDefaultValueSql("(now() at time zone 'utc')")
            .ValueGeneratedOnAdd();

        builder
            .HasMany(x => x.Notes)
            .WithOne(x => x.User)
            .IsRequired();

        builder
            .HasMany(x => x.SpecialSomeones)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

internal sealed class SpecialSomeoneModelConfigurations : IEntityTypeConfiguration<SpecialSomeone>
{
    public void Configure(EntityTypeBuilder<SpecialSomeone> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .HasAlternateKey(x => x.UniqueIdentifier);

        builder
            .HasMany(x => x.Notes);
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions;

public static class SpecialSomeoneModelBuilderExtensions
{
    public static ModelBuilder ConfigureSpecialSomeoneModel(this ModelBuilder builder)
    {
        builder
            .Entity<SpecialSomeone>()
            .HasKey(x => x.Id);

        builder
            .Entity<SpecialSomeone>()
            .HasMany(x => x.Notes);
        
        return builder;
    }
}
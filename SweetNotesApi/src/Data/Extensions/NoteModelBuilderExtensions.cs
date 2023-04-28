using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions;

public static class NoteModelBuilderExtensions
{
    public static ModelBuilder ConfigureNoteModel(this ModelBuilder builder)
    {
        builder
            .Entity<Note>()
            .HasKey(x => x.Id);

        builder
            .Entity<Note>()
            .Property(x => x.CreatedUtc)
            .HasDefaultValueSql("(now() at time zone 'utc')")
            .ValueGeneratedOnAdd();

        builder
            .Entity<Note>()
            .HasIndex(x => x.CreatedUtc, "IX_Note_CreatedUTC")
            .IsDescending();

        builder
            .Entity<Note>()
            .HasOne(x => x.SpecialSomeone);
        
        return builder;
    }
}
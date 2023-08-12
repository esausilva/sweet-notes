using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public sealed class NoteModelConfigurations : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.CreatedUTC)
            .HasDefaultValueSql("(now() at time zone 'utc')")
            .ValueGeneratedOnAdd();

        builder
            .HasIndex(x => x.CreatedUTC, "IX_Note_CreatedUTC")
            .IsDescending();

        builder
            .HasOne(x => x.SpecialSomeone);
    }
}
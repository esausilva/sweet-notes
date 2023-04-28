using Data.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Config;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureUserModel()
            .ConfigureNoteModel()
            .ConfigureSpecialSomeoneModel();
    }

    public DbSet<Note> Notes { get; set; } = default!;
}
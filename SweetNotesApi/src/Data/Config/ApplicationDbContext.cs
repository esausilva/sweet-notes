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
        // TODO: Refactor model builders
        // https://www.youtube.com/watch?v=v19arLqQkP8&list=WL&index=9 20:22, 25:16
        modelBuilder
            .ConfigureUserModel()
            .ConfigureNoteModel()
            .ConfigureSpecialSomeoneModel();
    }

    public DbSet<Note> Notes { get; set; } = default!;
}
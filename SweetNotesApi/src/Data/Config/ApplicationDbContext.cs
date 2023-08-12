using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Config;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Note> Notes { get; set; } = default!;
    public DbSet<SpecialSomeone> SpecialSomeone { get; set; } = default!;
}
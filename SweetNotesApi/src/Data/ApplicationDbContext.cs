using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Note> Notes => Set<Note>();
    public DbSet<SpecialSomeone> SpecialSomeone => Set<SpecialSomeone>();
}
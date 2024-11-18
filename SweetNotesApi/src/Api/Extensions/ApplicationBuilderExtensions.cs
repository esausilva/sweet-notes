using Api.Exceptions;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var dbContextFactory = scope
            .ServiceProvider
            .GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
        var dbContext = dbContextFactory.CreateDbContext();
        
        dbContext.Database.Migrate();
    }
    
    public static void EnsureDbMigrated(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var dbContextFactory = scope
            .ServiceProvider
            .GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
        var dbContext = dbContextFactory.CreateDbContext();
        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        
        if (pendingMigrations.Any())
            throw new PendingDbMigrationsException("Pending migrations.");
        
        Console.WriteLine("No pending migrations."); // TODO: Refactor to log
    }
}
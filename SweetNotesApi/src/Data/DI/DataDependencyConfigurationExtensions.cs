using Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.DI;

public static class DataDependencyConfigurationExtensions
{
    public static IServiceCollection ConfigureDataDependencies
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddPooledDbContextFactory<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SweetNotes"))
            );
        
        return services;
    }
}
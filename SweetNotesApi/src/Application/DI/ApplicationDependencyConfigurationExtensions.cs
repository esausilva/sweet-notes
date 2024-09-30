using System.Reflection;
using Application.Commands;
using Application.Providers;
using Application.Providers.SnowflakeId;
using Application.Providers.Ulid;
using Application.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class ApplicationDependencyConfigurationExtensions
{
    public static IServiceCollection ConfigureApplicationDependencies
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(item => item.GetInterfaces()
                .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(ICommandRequest<,>)) && !item.IsAbstract && !item.IsInterface)
            .ToList()
            .ForEach(assignedTypes => {
                var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(ICommandRequest<,>));
                services.AddTransient(serviceType, assignedTypes);
            });
        
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(item => item.GetInterfaces()
                .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IQueryRequest<,>)) && !item.IsAbstract && !item.IsInterface)
            .ToList()
            .ForEach(assignedTypes => {
                var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IQueryRequest<,>));
                services.AddTransient(serviceType, assignedTypes);
            });

        // Keeping for posterity
        // services
        //     .AddSingleton<IUniqueIdProvider<long>, SnowflakeIdProvider>();
        
        services
            .AddSingleton<IUniqueIdProvider<Ulid>, UlidProvider>();
        
        return services;
    }
}
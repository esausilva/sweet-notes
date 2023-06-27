using Api.Auth.Services.ClaimsReaderService;

namespace Api.DI;

public static class ApiDependencyConfigurationExtensions
{
    public static ConfigurationManager GetApiConfigurations(this ConfigurationManager configuration)
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory());
        configuration.AddJsonFile("appsettings.json");
        
        Console.WriteLine(configuration.GetDebugView());
        
        return configuration;
    }

    public static IServiceCollection ConfigureApiDependencies
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddSingleton<IClaimsReader, ClaimsReader>();

        return services;
    }
}
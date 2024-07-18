using Api.Auth.Services.ClaimsReaderService;
using Api.Security;
using Api.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Api.DI;

public static class ApiDependencyConfigurationExtensions
{
    public static ConfigurationManager GetApiConfigurations(this ConfigurationManager configuration)
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory());
        configuration.AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();
        
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
            .AddScoped<IClaimsReader, ClaimsReader>()
            .AddValidatorsFromAssemblyContaining<CreateSpecialSomeoneCommandValidator>();

        return services;
    }
}
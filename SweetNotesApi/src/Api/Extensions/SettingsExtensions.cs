using Api.Security;

namespace Api.Extensions;

public static class SettingsExtensions
{
    public static IServiceCollection ConfigureSettings
    (
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services
            .Configure<CorsSettings>(configuration.GetSection(nameof(CorsSettings)));

        return services;
    } 
}
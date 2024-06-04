using Microsoft.AspNetCore.Cors.Infrastructure;
using Api.Constants;

namespace Api.Security;

public static class Cors
{
    public const string FrontEndPolicyName = "AllowFrontEnd";

    public static Action<CorsOptions> Configure(IConfiguration configuration)
    {
        var corsSettings = configuration
            .GetSection(nameof(CorsSettings))
            .Get<CorsSettings>();
        var frontendOrigin = corsSettings?.FrontendOrigin;
        
        ArgumentException.ThrowIfNullOrWhiteSpace(frontendOrigin, nameof(frontendOrigin));
        
        var origins = new List<string?>
        {
            frontendOrigin,
            corsSettings?.FrontendOriginLocalNetwork
        };

        return options =>
        {
            options
                .AddPolicy(name: FrontEndPolicyName,
                    policy =>
                    {
                        policy
                            .WithOrigins(origins.Where(o => o is not null).ToArray()!)
                            .AllowAnyHeader()
                            .AllowCredentials();
                        policy.WithMethods(HttpVerbs.Post, HttpVerbs.Get);
                    }
                );
        };
    }
}
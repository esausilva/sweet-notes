using Microsoft.AspNetCore.Cors.Infrastructure;
using Api.Constants;

namespace Api.Security;

public static class Cors
{
    public const string FrontEndPolicyName = "AllowFrontEnd";

    public static Action<CorsOptions> Configure(IConfiguration configuration)
    {
        var frontendOrigin = configuration
            .GetSection(nameof(CorsSettings))
            .Get<CorsSettings>()?
            .FrontendOrigin;
        
        // TODO: Refactor to .ThrowIfNullOrWhiteSpace when migrating to .NET 8
        ArgumentException.ThrowIfNullOrEmpty(frontendOrigin, nameof(frontendOrigin));

        return options =>
        {
            options
                .AddPolicy(name: FrontEndPolicyName,
                    policy =>
                    {
                        policy
                            .WithOrigins(frontendOrigin)
                            .AllowAnyHeader()
                            .AllowCredentials();
                        policy.WithMethods(HttpVerbs.Post, HttpVerbs.Get);
                    }
                );
        };
    }
}
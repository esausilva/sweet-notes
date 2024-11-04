using Api.Security;
using Microsoft.Extensions.Options;

namespace Api.RestEndpoints.UserEps;

public static class UserEndpointBuilder
{
    public static RouteGroupBuilder MapUserEndpoint(this RouteGroupBuilder group, WebApplication app)
    {
        var corsSettings = app.Services.GetRequiredService<IOptions<CorsSettings>>().Value;

        group.MapPost("signup", UserSignup.PostAsync);
        group.MapPost("login", UserLogin.PostAsync);
        group.MapPost("update-password", UserUpdatePassword.PostAsync);
        
        group.MapGet("logout", httpContext => UserLogout.GetAsync(httpContext, corsSettings))
            .RequireAuthorization();
        group.MapGet("me", UserMe.GetAsync);

        return group;
    }
}
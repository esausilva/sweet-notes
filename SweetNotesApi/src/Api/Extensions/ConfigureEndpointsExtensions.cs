using Api.RestEndpoints.SpecialSomeoneEndpoints;
using Api.RestEndpoints.UserEndpoints;

namespace Api.Extensions;

public static class ConfigureEndpointsExtensions
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        app.MapGroup("/user")
            .MapUserEndpoint();
        app.MapGet("special-someone-name/{identifier}", SpecialSomeoneName.GetAsync);
    }
}
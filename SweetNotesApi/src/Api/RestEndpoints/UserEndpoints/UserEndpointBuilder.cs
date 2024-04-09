namespace Api.RestEndpoints.UserEndpoints;

public static class UserEndpointBuilder
{
    public static RouteGroupBuilder MapUserEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("signup", UserSignup.PostAsync);
        group.MapPost("login", UserLogin.PostAsync);
        group.MapGet("logout", UserLogout.GetAsync);
        group.MapGet("me", UserMe.GetAsync);

        return group;
    }
}
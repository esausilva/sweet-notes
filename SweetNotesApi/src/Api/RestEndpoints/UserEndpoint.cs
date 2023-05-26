using Api.Models;
using Application.Commands;

namespace Api.RestEndpoints;

public static class UserEndpoint
{
    public static WebApplication MapUserEndpoint(this WebApplication app)
    {
        app.MapPost("api/signup", PostUserSignupAsync);

        return app;
    }

    private static async Task<IResult> PostUserSignupAsync(CreateUserSignup userSignup, ICommandRequest<CreateUserSignupCommand, int> commandRequest)
    {
        var command = new CreateUserSignupCommand
        (
            FirstName: userSignup.FirstName,
            LastName: userSignup.LastName,
            EmailAddress: userSignup.EmailAddress,
            Password: userSignup.Password
        );

        var userId = await commandRequest.Handle(command);

        return Results.Ok(new { userId });
    }
}
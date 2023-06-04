using Api.Models;
using Application.Commands;
using Application.Commands.UserSignup;

namespace Api.RestEndpoints;

public static class UserEndpoint
{
    public static WebApplication MapUserEndpoint(this WebApplication app)
    {
        app.MapPost("api/signup", PostUserSignupAsync);

        return app;
    }

    private static async Task<IResult> PostUserSignupAsync
    (
        CreateUserSignup userSignup, 
        ICommandRequest<CreateUserSignupCommand, int> commandRequest, 
        CancellationToken cancellationToken
    )
    {
        var command = new CreateUserSignupCommand
        (
            FirstName: userSignup.FirstName,
            LastName: userSignup.LastName,
            EmailAddress: userSignup.EmailAddress,
            Password: userSignup.Password
        );

        var userId = await commandRequest.Handle(command, cancellationToken);

        return Results.Ok(new { userId });
    }
}
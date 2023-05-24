using Api.Models;
using Application.Commands;
using Domain.Entities;

namespace Api.RestEndpoints;

public static class UserEndpoint
{
    public static WebApplication MapUserEndpoint(this WebApplication app)
    {
        app.MapPost("api/signup", PostUserSignupAsync);

        return app;
    }

    private static async Task<IResult> PostUserSignupAsync(UserSignupModel signupModel, ICommandRequest<User> commandRequest)
    {
        var user = new User
        {
            FirstName = signupModel.FirstName,
            LastName = signupModel.LastName,
            EmailAddress = signupModel.EmailAddress,
            Password = signupModel.Password
        };

        await commandRequest.Handle(user);

        return Results.Ok(user);
    }
}
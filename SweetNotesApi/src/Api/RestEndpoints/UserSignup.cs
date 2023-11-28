using Api.Exceptions;
using Api.RestEndpoints.Models;
using Application.Commands;
using Application.Commands.UserSignup;
using FluentValidation;
using static Api.RestEndpoints.Helpers.UserEndpointHelpers;

namespace Api.RestEndpoints;

public static class UserSignup
{
    public static async Task<IResult> PostAsync
    (
        IValidator<CreateUserSignup> validator,
        HttpContext context,
        CreateUserSignup userSignup, 
        ICommandRequest<CreateUserSignupCommand, int> commandRequest, 
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(userSignup, cancellationToken);
        if (validationResult.IsValid is false) 
            throw new ApiValidationException(validationResult.ToDictionary());

        var command = new CreateUserSignupCommand
        (
            FirstName: userSignup.FirstName,
            LastName: userSignup.LastName,
            EmailAddress: userSignup.EmailAddress,
            Password: userSignup.Password
        );
        var userId = await commandRequest.Handle(command, cancellationToken);
        
        await CreateSignin(context, userSignup.EmailAddress, userSignup.FirstName, userSignup.LastName, userId.ToString());

        return Results.Ok();
    }
}
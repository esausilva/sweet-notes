using Api.Exceptions;
using Api.RestEndpoints.UserEps.Models;
using Application.Commands;
using Application.Commands.UserSignup;
using FluentValidation;
using static Api.RestEndpoints.Helpers.UserEndpointHelpers;

namespace Api.RestEndpoints.UserEps;

public static class UserSignup
{
    public static async Task<IResult> PostAsync
    (
        IValidator<CreateUserSignupRequest> validator,
        HttpContext context,
        CreateUserSignupRequest request, 
        ICommandRequest<CreateUserSignupCommand, int> commandRequest, 
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid is false) 
            throw new ApiValidationException(validationResult.ToDictionary());

        var command = new CreateUserSignupCommand
        (
            FirstName: request.FirstName,
            LastName: request.LastName,
            EmailAddress: request.EmailAddress,
            Password: request.Password
        );
        var userId = await commandRequest.Handle(command, cancellationToken);
        
        await CreateSignin(context, request.EmailAddress, request.FirstName, request.LastName, userId.ToString());

        return Results.Ok(new {});
    }
}
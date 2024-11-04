using Api.Auth.Services.ClaimsReaderService;
using Api.Exceptions;
using Api.RestEndpoints.UserEps.Models;
using Application.Commands;
using Application.Commands.UserUpdatePassword;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace Api.RestEndpoints.UserEps;

public static class UserUpdatePassword
{
    [Authorize]
    public static async Task<IResult> PostAsync
    (
        IValidator<UserUpdatePasswordRequest> validator,
        HttpContext context,
        UserUpdatePasswordRequest request,
        ICommandRequest<UserUpdatePasswordCommand, bool> commandRequest,
        IClaimsReader claimsReader,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid is false)
            throw new ApiValidationException(validationResult.ToDictionary());
        
        var command = new UserUpdatePasswordCommand
        (
            EmailAddress: claimsReader.GetClaims(context.User).EmailAddress,
            CurrentPassword: request.CurrentPassword,
            NewPassword: request.NewPassword
        );
        await commandRequest.Handle(command, cancellationToken);
        
        return Results.Ok(new {});
    }
}
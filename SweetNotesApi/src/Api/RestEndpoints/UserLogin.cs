using Api.Exceptions;
using Application.Queries;
using Application.Queries.UserLogin;
using Domain.Entities;
using FluentValidation;
using static Api.RestEndpoints.Helpers.UserEndpointHelpers;

namespace Api.RestEndpoints;

public static class UserLogin
{
    public static async Task<IResult> PostAsync
    (
        IValidator<Models.UserLogin> validator,
        HttpContext context,
        Models.UserLogin userLogin,
        IQueryRequest<UserLoginQuery, User?> queryRequest,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(userLogin, cancellationToken);
        if (validationResult.IsValid is false)
            throw new ApiValidationException(validationResult.ToDictionary());
        
        var query = new UserLoginQuery(userLogin.EmailAddress, userLogin.Password);
        var user = await queryRequest.Handle(query, cancellationToken);
        
        if (user is null)
            throw new UnauthorizedException();
        
        await CreateSignin(context, userLogin.EmailAddress, user.FirstName, user.LastName, user.Id.ToString());

        return Results.Ok();
    }
}
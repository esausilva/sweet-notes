using Api.Exceptions;
using Api.RestEndpoints.UserEps.Models;
using Application.Queries;
using Application.Queries.UserLogin;
using Domain.Entities;
using FluentValidation;
using static Api.RestEndpoints.Helpers.UserEndpointHelpers;

namespace Api.RestEndpoints.UserEps;

public static class UserLogin
{
    public static async Task<IResult> PostAsync
    (
        IValidator<UserLoginRequest> validator,
        HttpContext context,
        UserLoginRequest request,
        IQueryRequest<UserLoginQuery, User?> queryRequest,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid is false)
            throw new ApiValidationException(validationResult.ToDictionary());
        
        var query = new UserLoginQuery(request.EmailAddress, request.Password);
        var user = await queryRequest.Handle(query, cancellationToken);
        
        if (user is null)
            throw new UnauthorizedException();
        
        await CreateSignin(context, request.EmailAddress, user.FirstName, user.LastName, user.Id.ToString());

        return Results.Ok(new {});
    }
}
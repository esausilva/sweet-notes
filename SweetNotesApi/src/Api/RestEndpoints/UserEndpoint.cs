using System.Security.Claims;
using Api.Auth;
using Api.Exceptions;
using Api.Models;
using Application.Commands;
using Application.Commands.UserLogin;
using Application.Commands.UserSignup;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Api.RestEndpoints;

public static class UserEndpoint
{
    public static RouteGroupBuilder MapUserEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("signup", PostUserSignupAsync);
        group.MapPost("login", PostUserLoginAsync);
        group.MapGet("logout", GetUserLogoutAsync);

        return group;
    }

    [Authorize]
    private static async Task GetUserLogoutAsync(HttpContext context)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static async Task<IResult> PostUserSignupAsync
    (
        HttpContext context,
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
        
        await CreateSignin(context, userSignup.EmailAddress, userSignup.FirstName, userSignup.LastName, userId.ToString());

        return Results.Ok();
    }

    private static async Task<IResult> PostUserLoginAsync
    (
        HttpContext context,
        UserLogin userLogin,
        ICommandRequest<UserLoginCommand, User?> commandRequest,
        CancellationToken cancellationToken
    )
    {
        var command = new UserLoginCommand(userLogin.EmailAddress, userLogin.Password);
        var user = await commandRequest.Handle(command, cancellationToken);
        
        if (user is null)
        {
            throw new UnauthorizedException();
        }
        
        await CreateSignin(context, userLogin.EmailAddress, user.FirstName, user.LastName, user.Id.ToString());

        return Results.Ok();
    }

    private static async Task CreateSignin
    (
        HttpContext context, 
        string emailAddress, 
        string firstName,
        string lastName,
        string userId
    )
    {
        var claimsIdentity = new ClaimsIdentity(
            identity: new AuthenticationIdentity
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = $"{firstName} {lastName}"
            },
            claims: new[]
            {
                new Claim(ClaimTypes.Email, emailAddress),
                new Claim(ClaimTypes.Sid, userId),
                new Claim(ClaimTypes.Role, "User")
            },
            authenticationType: CookieAuthenticationDefaults.AuthenticationScheme,
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role
        );

        await context.SignInAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            principal: new ClaimsPrincipal(claimsIdentity),
            properties: new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
            });
    }
}
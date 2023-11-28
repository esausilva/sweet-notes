using System.Security.Claims;
using Api.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Api.RestEndpoints.Helpers;

public static class UserEndpointHelpers
{
    public static async Task CreateSignin
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
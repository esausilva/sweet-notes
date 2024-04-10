using Api.Auth.Services.ClaimsReaderService;
using Api.RestEndpoints.UserEps.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api.RestEndpoints.UserEps;

public static class UserMe
{
    [Authorize]
    public static Task<UserMeResponse> GetAsync(HttpContext context, IClaimsReader claimsReader)
    {
        var claims = claimsReader.GetClaims(context.User);

        return Task.FromResult(new UserMeResponse(claims.Name, claims.EmailAddress));
    }
}
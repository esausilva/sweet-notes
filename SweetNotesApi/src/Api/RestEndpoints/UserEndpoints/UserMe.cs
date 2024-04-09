using Api.Auth.Services.ClaimsReaderService;
using Api.RestEndpoints.UserEndpoints.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api.RestEndpoints.UserEndpoints;

public static class UserMe
{
    [Authorize]
    public static Task<UserMeResponse> GetAsync(HttpContext context, IClaimsReader claimsReader)
    {
        var claims = claimsReader.GetClaims(context.User);

        return Task.FromResult(new UserMeResponse(claims.Name, claims.EmailAddress));
    }
}
using Api.Auth.Claims.Configuration;
using Api.Auth.Services.ClaimsReaderService;
using Microsoft.AspNetCore.Authorization;

namespace Api.RestEndpoints;

public static class UserMe
{
    [Authorize]
    public static Task<UserClaims> GetAsync(HttpContext context, IClaimsReader claimsReader)
    {
        var claims = claimsReader.GetClaims(context.User);

        return Task.FromResult(claims);
    }
}
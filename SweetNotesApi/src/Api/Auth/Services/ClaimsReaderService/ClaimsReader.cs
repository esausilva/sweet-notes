using System.Security.Claims;
using Api.Auth.Claims.Configuration;

namespace Api.Auth.Services.ClaimsReaderService;

public sealed class ClaimsReader : IClaimsReader
{
    UserClaims IClaimsReader.GetClaims(ClaimsPrincipal claimsPrincipal)
    {
        var userName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
        int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.Sid), out var userId);

        return new UserClaims(userId, userName!, email!, role!);
    }
}
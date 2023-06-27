using System.Security.Claims;
using Api.Auth.Claims.Configuration;

namespace Api.Auth.Services.ClaimsReaderService;

public interface IClaimsReader
{
    UserClaims GetClaims(ClaimsPrincipal claimsPrincipal);
}
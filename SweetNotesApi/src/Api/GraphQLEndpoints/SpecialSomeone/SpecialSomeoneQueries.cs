using System.Security.Claims;
using Api.Auth.Services.ClaimsReaderService;
using Application.Queries.SpecialSomeone;
using HotChocolate.Authorization;

namespace Api.GraphQLEndpoints.SpecialSomeone;

[ExtendObjectType(Name = "Query")]
public class SpecialSomeoneQueries
{
    private readonly IClaimsReader _claimsReader;

    public SpecialSomeoneQueries(IClaimsReader claimsReader)
    {
        _claimsReader = claimsReader;
    }
    
    [Authorize]
    public async Task<Domain.Entities.SpecialSomeone[]> GetSpecialSomeonesForUser
    (
        ClaimsPrincipal claimsPrincipal,
        SpecialSomeoneByUserIdDataLoader specialSomeonesByUserId,
        CancellationToken cancellationToken
    )
    {
        var userId = _claimsReader.GetClaims(claimsPrincipal).UserId;

        return await specialSomeonesByUserId.LoadAsync(userId, cancellationToken);
    }
}
using System.Security.Claims;
using Api.Auth.Services.ClaimsReaderService;
using Application.Commands;
using Application.Commands.SpecialSomeone;
using HotChocolate.Authorization;

namespace Api.GraphQLEndpoints.SpecialSomeone;

[ExtendObjectType(Name = "Mutation")]
public class SpecialSomeoneMutations
{
    private readonly ICommandRequest<(CreateSpecialSomeoneCommand command, int userId), Domain.Entities.SpecialSomeone> _commandRequest;
    private readonly IClaimsReader _claimsReader;

    public SpecialSomeoneMutations
    (
        ICommandRequest<(CreateSpecialSomeoneCommand command, int userId), Domain.Entities.SpecialSomeone> commandRequest,
        IClaimsReader claimsReader
    )
    {
        _commandRequest = commandRequest;
        _claimsReader = claimsReader;
    }
    
    [Authorize]
    public async Task<CreateSpecialSomeonePayload> CreateSpecialSomeone
    (
        CreateSpecialSomeoneCommand input,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken
    )
    {
        var userId = _claimsReader.GetClaims(claimsPrincipal).UserId;
        var specialSomeone = await _commandRequest.Handle((input, userId), cancellationToken);
        
        return new CreateSpecialSomeonePayload(specialSomeone);
    }
}
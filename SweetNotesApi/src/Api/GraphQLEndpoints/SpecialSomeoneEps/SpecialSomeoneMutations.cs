using System.Security.Claims;
using Api.Auth.Services.ClaimsReaderService;
using Api.GraphQLEndpoints.SpecialSomeoneEps.ResponsePayload;
using Application.Commands;
using Application.Commands.CreateSpecialSomeone;
using Domain.Entities;
using HotChocolate.Authorization;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps;

[ExtendObjectType(Name = "Mutation")]
public class SpecialSomeoneMutations
{
    private readonly ICommandRequest<CreateSpecialSomeoneCommand, SpecialSomeone> _commandRequest;
    private readonly IClaimsReader _claimsReader;

    public SpecialSomeoneMutations
    (
        ICommandRequest<CreateSpecialSomeoneCommand, SpecialSomeone> commandRequest,
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
        input.UserId = _claimsReader.GetClaims(claimsPrincipal).UserId;
        var specialSomeone = await _commandRequest.Handle(input, cancellationToken);
        
        return new CreateSpecialSomeonePayload(specialSomeone);
    }
}
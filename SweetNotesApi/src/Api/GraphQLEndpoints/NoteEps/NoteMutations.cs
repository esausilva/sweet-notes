using System.Security.Claims;
using Api.Auth.Services.ClaimsReaderService;
using Api.GraphQLEndpoints.NoteEps.ResponsePayload;
using Application.Commands;
using Application.Commands.CreateNote;
using Domain.Entities;
using HotChocolate.Authorization;

namespace Api.GraphQLEndpoints.NoteEps;

[ExtendObjectType(Name = "Mutation")]
public class NoteMutations
{
    private readonly ICommandRequest<CreateNoteCommand, Note> _commandRequest;
    private readonly IClaimsReader _claimsReader;

    public NoteMutations(IClaimsReader claimsReader, ICommandRequest<CreateNoteCommand, Note> commandRequest)
    {
        _claimsReader = claimsReader;
        _commandRequest = commandRequest;
    }

    [Authorize]
    public async Task<CreateNotePayload> CreateNote
    (
        CreateNoteCommand input,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken
    )
    {
        input.UserId = _claimsReader.GetClaims(claimsPrincipal).UserId;
        var note = await _commandRequest.Handle(input, cancellationToken);

        return new CreateNotePayload(note);
    }
}
using System.Security.Claims;
using Api.Auth.Services.ClaimsReaderService;
using Api.GraphQLEndpoints.NoteEps.ResponsePayload;
using Application.Commands;
using Application.Commands.CreateNote;
using Application.Commands.DeleteNote;
using Domain.Entities;
using HotChocolate.Authorization;

namespace Api.GraphQLEndpoints.NoteEps;

[ExtendObjectType("Mutation")]
public sealed class NoteMutations
{
    private readonly ICommandRequest<CreateNoteCommand, Note> _createNoteCommandRequest;
    private readonly ICommandRequest<DeleteNoteCommand, bool> _deleteNoteCommandRequest;
    private readonly IClaimsReader _claimsReader;

    public NoteMutations
    (
        IClaimsReader claimsReader, 
        ICommandRequest<CreateNoteCommand, Note> createNoteCommandRequest, 
        ICommandRequest<DeleteNoteCommand, bool> deleteNoteCommandRequest
    )
    {
        _claimsReader = claimsReader;
        _createNoteCommandRequest = createNoteCommandRequest;
        _deleteNoteCommandRequest = deleteNoteCommandRequest;
    }

    [Authorize]
    public async Task<NotePayload> CreateNote
    (
        CreateNoteCommand input,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken
    )
    {
        input.UserId = _claimsReader.GetClaims(claimsPrincipal).UserId;
        var note = await _createNoteCommandRequest.Handle(input, cancellationToken);

        return new NotePayload(note);
    }

    [Authorize]
    public async Task<NotePayload> DeleteNote
    (
        DeleteNoteCommand input,
        CancellationToken cancellationToken
    )
    {
        await _deleteNoteCommandRequest.Handle(input, cancellationToken);
        
        return new NotePayload();
    }
}
using Api.GraphQLEndpoints.Common;
using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps.ResponsePayload;

public sealed class NotePayload : NotePayloadBase
{
    public NotePayload(Note? note = default) 
        : base(note)
    { }

    public NotePayload(IReadOnlyList<UserError> errors) 
        : base(errors)
    { }
}
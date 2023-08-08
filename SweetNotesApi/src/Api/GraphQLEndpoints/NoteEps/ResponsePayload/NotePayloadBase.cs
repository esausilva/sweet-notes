using Api.GraphQLEndpoints.Common;
using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps.ResponsePayload;

public class NotePayloadBase : Payload
{
    public Note Note { get; }

    protected NotePayloadBase(Note note)
    {
        Note = note;
    }

    protected NotePayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    { }
}
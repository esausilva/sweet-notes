using Api.GraphQLEndpoints.Common;
using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps;

public class CreateNotePayload : NotePayloadBase
{
    public CreateNotePayload(Note note) 
        : base(note)
    { }

    public CreateNotePayload(IReadOnlyList<UserError> errors) 
        : base(errors)
    { }
}
using Application.Commands.CreateNote;

namespace Api.GraphQLEndpoints.NoteEps.Types;

public class CreateNoteInputType : InputObjectType<CreateNoteCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateNoteCommand> descriptor)
    {
        descriptor
            .Field(x => x.UserId)
            .Ignore();
    }
}
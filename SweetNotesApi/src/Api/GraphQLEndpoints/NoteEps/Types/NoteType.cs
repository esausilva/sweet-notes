using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps.Types;

public class NoteType : ObjectType<Note>
{
    protected override void Configure(IObjectTypeDescriptor<Note> descriptor)
    {
        descriptor
            .Field(x => x.UserId)
            .Ignore();
        
        descriptor
            .Field(x => x.SpecialSomeoneId)
            .Ignore();
    }
}
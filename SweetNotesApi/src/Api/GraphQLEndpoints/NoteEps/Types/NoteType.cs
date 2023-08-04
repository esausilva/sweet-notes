using Api.GraphQLEndpoints.NoteEps.Resolvers;
using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps.Types;

public sealed class NoteType : ObjectType<Note>
{
    protected override void Configure(IObjectTypeDescriptor<Note> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(x => x.Id);
        
        descriptor
            .Field(x => x.UserId)
            .Ignore();
        
        descriptor
            .Field(x => x.SpecialSomeoneId)
            .Ignore();
        
        descriptor
            .Field(x => x.SpecialSomeone)
            .ResolveWith<NoteResolvers>(t => 
                t.GetSpecialSomeoneAsync(default!, default!, default!, default)
            );
        
        descriptor
            .Field(x => x.User)
            .ResolveWith<NoteResolvers>(t => 
                t.GetUserAsync(default!, default!, default!, default)
            );
    }
}
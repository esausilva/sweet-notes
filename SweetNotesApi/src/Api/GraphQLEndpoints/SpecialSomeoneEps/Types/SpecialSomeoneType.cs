using Api.GraphQLEndpoints.SpecialSomeoneEps.Resolvers;
using Domain.Entities;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps.Types;

public class SpecialSomeoneType : ObjectType<SpecialSomeone>
{
    protected override void Configure(IObjectTypeDescriptor<SpecialSomeone> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(x => x.Id);
        
        descriptor
            .Field(x => x.UserId)
            .Ignore();
        
        descriptor
            .Field(x => x.User)
            .ResolveWith<SpecialSomeoneResolvers>(t => 
                t.GetUserAsync(default!, default!, default!, default)
            );
        
        descriptor
            .Field(x => x.Notes)
            .ResolveWith<SpecialSomeoneResolvers>(t => 
                t.GetNotesAsync(default!, default!, default!, default)
            );
    }
}
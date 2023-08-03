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
    }
}
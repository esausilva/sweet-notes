namespace Api.GraphQLEndpoints.SpecialSomeone;

public class SpecialSomeoneType : ObjectType<Domain.Entities.SpecialSomeone>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Entities.SpecialSomeone> descriptor)
    {
        descriptor
            .Field(x => x.UserId)
            .Ignore();
    }
}
namespace Api.GraphQLEndpoints.User;

public class UserType : ObjectType<Domain.Entities.User>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Entities.User> descriptor)
    {
        descriptor
            .Field(x => x.Password)
            .Ignore();
        
        descriptor
            .Field(x => x.SignedUpUTC)
            .Ignore();
    }
}
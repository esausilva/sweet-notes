using Domain.Entities;

namespace Api.GraphQLEndpoints.UserEps;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .Field(x => x.Password)
            .Ignore();
        
        descriptor
            .Field(x => x.SignedUpUTC)
            .Ignore();
    }
}
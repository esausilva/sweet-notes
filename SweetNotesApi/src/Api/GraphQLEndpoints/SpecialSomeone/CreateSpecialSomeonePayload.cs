using Api.GraphQLEndpoints.Common;

namespace Api.GraphQLEndpoints.SpecialSomeone;

public class CreateSpecialSomeonePayload : SpecialSomeonePayloadBase
{
    public CreateSpecialSomeonePayload(Domain.Entities.SpecialSomeone specialSomeone) 
        : base(specialSomeone)
    { }

    public CreateSpecialSomeonePayload(UserError error) 
        : base(new[] { error })
    { }
}
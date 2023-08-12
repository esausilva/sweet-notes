using Api.GraphQLEndpoints.Common;
using Domain.Entities;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps.ResponsePayload;

public sealed class CreateSpecialSomeonePayload : SpecialSomeonePayloadBase
{
    public CreateSpecialSomeonePayload(SpecialSomeone specialSomeone) 
        : base(specialSomeone)
    { }

    public CreateSpecialSomeonePayload(UserError error) 
        : base(new[] { error })
    { }
}
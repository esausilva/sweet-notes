using Api.GraphQLEndpoints.Common;

namespace Api.GraphQLEndpoints.SpecialSomeone;

public class SpecialSomeonePayloadBase : Payload
{
    public Domain.Entities.SpecialSomeone SpecialSomeone { get; }

    protected SpecialSomeonePayloadBase(Domain.Entities.SpecialSomeone specialSomeone)
    {
        SpecialSomeone = specialSomeone;
    }

    protected SpecialSomeonePayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    { }
}
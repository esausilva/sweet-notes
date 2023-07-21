using Api.GraphQLEndpoints.Common;
using Domain.Entities;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps;

public class SpecialSomeonePayloadBase : Payload
{
    public SpecialSomeone SpecialSomeone { get; }

    protected SpecialSomeonePayloadBase(SpecialSomeone specialSomeone)
    {
        SpecialSomeone = specialSomeone;
    }

    protected SpecialSomeonePayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    { }
}
using Application.Commands.SpecialSomeone;

namespace Api.GraphQLEndpoints.SpecialSomeone;

[ExtendObjectType(Name = "Mutation")]
public class SpecialSomeoneMutations
{
    public async Task<CreateSpecialSomeonePayload> CreateSpecialSomeone
    (
        CreateSpecialSomeoneCommand input,
        CancellationToken cancellationToken
    )
    {
        return new CreateSpecialSomeonePayload(new Domain.Entities.SpecialSomeone());
    }
}
using Application.Commands;
using Application.Commands.SpecialSomeone;

namespace Api.GraphQLEndpoints.SpecialSomeone;

[ExtendObjectType(Name = "Mutation")]
public class SpecialSomeoneMutations
{
    private readonly ICommandRequest<CreateSpecialSomeoneCommand, Domain.Entities.SpecialSomeone> _commandRequest;

    public SpecialSomeoneMutations(ICommandRequest<CreateSpecialSomeoneCommand, Domain.Entities.SpecialSomeone> commandRequest)
    {
        _commandRequest = commandRequest;
    }
    
    public async Task<CreateSpecialSomeonePayload> CreateSpecialSomeone
    (
        CreateSpecialSomeoneCommand input,
        CancellationToken cancellationToken
    )
    {
        var specialSomeone = await _commandRequest.Handle(input, cancellationToken);
        
        return new CreateSpecialSomeonePayload(specialSomeone);
    }
}
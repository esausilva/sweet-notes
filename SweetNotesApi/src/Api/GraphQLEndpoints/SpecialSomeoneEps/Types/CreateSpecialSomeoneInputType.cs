using Application.Commands.CreateSpecialSomeone;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps.Types;

public class CreateSpecialSomeoneInputType : InputObjectType<CreateSpecialSomeoneCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateSpecialSomeoneCommand> descriptor)
    {
        descriptor
            .Field(x => x.UserId)
            .Ignore();
    }
}
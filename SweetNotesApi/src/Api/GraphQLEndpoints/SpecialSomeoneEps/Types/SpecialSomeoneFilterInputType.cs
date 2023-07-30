using Domain.Entities;
using HotChocolate.Data.Filters;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps.Types;

public class SpecialSomeoneFilterInputType : FilterInputType<SpecialSomeone>
{
    protected override void Configure(IFilterInputTypeDescriptor<SpecialSomeone> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.UniqueIdentifier);
    }
}
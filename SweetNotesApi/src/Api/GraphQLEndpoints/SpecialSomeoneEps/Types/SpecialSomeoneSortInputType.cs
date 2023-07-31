using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps.Types;

public class SpecialSomeoneSortInputType : SortInputType<SpecialSomeone>
{
    protected override void Configure(ISortInputTypeDescriptor<SpecialSomeone> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.FirstName);
        descriptor.Field(x => x.LastName);
    }
}
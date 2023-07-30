using Domain.Entities;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace Api.GraphQLEndpoints.NoteEps.Types;

public class NoteSortInputType : SortInputType<Note>
{
    protected override void Configure(ISortInputTypeDescriptor<Note> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.CreatedUTC);
    }
}
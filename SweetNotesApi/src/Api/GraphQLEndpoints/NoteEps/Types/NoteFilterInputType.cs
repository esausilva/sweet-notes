using Domain.Entities;
using HotChocolate.Data.Filters;

namespace Api.GraphQLEndpoints.NoteEps.Types;

public class NoteFilterInputType : FilterInputType<Note>
{
    protected override void Configure(IFilterInputTypeDescriptor<Note> descriptor)
    {
        descriptor.Ignore(x => x.Id);
        descriptor.Ignore(x => x.UserId);
        descriptor.Ignore(x => x.SpecialSomeoneId);
        descriptor.Ignore(x => x.User);
    }
}
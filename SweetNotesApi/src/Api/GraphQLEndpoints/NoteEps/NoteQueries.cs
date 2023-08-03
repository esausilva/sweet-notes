using Api.GraphQLEndpoints.NoteEps.Types;
using Data.Config;
using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps;

[ExtendObjectType(Name = "Query")]
public class NoteQueries
{
    // TODO: Figure out why User and SpecialSomeone are not loading. I think add data loaders, then bind to those fields in NoteType
    [UsePaging(IncludeTotalCount = true)]
    [UseFiltering(typeof(NoteFilterInputType))]
    [UseSorting(typeof(NoteSortInputType))]
    public IQueryable<Note> GetNotes (ApplicationDbContext context) => 
        context.Notes;
}
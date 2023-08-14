using Api.GraphQLEndpoints.NoteEps.Types;
using Data;
using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps;

[ExtendObjectType("Query")]
public sealed class NoteQueries
{
    [UsePaging(IncludeTotalCount = true)]
    [UseFiltering(typeof(NoteFilterInputType))]
    [UseSorting(typeof(NoteSortInputType))]
    public IQueryable<Note> GetNotes (ApplicationDbContext context) => 
        context.Notes;
}
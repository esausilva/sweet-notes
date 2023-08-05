using Application.Queries.GetNotes;
using Application.Queries.GetUser;
using Data.Config;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.GraphQLEndpoints.SpecialSomeoneEps.Resolvers;

internal sealed class SpecialSomeoneResolvers
{
    public async Task<User> GetUserAsync
    (
        [Parent] SpecialSomeone specialSomeone,
        ApplicationDbContext dbContext,
        UserByIdDataLoader userById,
        CancellationToken cancellationToken
    )
    {
        return await userById.LoadAsync(specialSomeone.UserId, cancellationToken);
    }
    
    public async Task<IEnumerable<Note>> GetNotesAsync
    (
        [Parent] SpecialSomeone specialSomeone,
        ApplicationDbContext dbContext,
        NoteByIdDataLoader noteById,
        CancellationToken cancellationToken
    )
    {
        var ids = await dbContext.SpecialSomeone
            .Where(x => x.Id == specialSomeone.Id)
            .Include(x => x.Notes)
            .SelectMany(x => x.Notes.Select(y => y.Id))
            .ToArrayAsync(cancellationToken);
        
        return await noteById.LoadAsync(ids, cancellationToken);
    }
}
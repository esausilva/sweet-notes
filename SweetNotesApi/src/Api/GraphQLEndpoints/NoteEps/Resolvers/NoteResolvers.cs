using Application.Queries.GetSpecialSomeone;
using Application.Queries.GetUser;
using Data.Config;
using Domain.Entities;

namespace Api.GraphQLEndpoints.NoteEps.Resolvers;

internal sealed class NoteResolvers
{
    public async Task<SpecialSomeone> GetSpecialSomeoneAsync
    (
        [Parent] Note note,
        ApplicationDbContext dbContext,
        SpecialSomeoneByIdDataLoader specialSomeoneById,
        CancellationToken cancellationToken
    )
    {
        return await specialSomeoneById.LoadAsync(note.SpecialSomeoneId, cancellationToken);
    }
    
    public async Task<User> GetUserAsync
    (
        [Parent] Note note,
        ApplicationDbContext dbContext,
        UserByIdDataLoader userById,
        CancellationToken cancellationToken
    )
    {
        return await userById.LoadAsync(note.UserId, cancellationToken);
    }
}
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetSpecialSomeone;

public sealed class SpecialSomeoneNameQueryHandler : IQueryRequest<SpecialSomeoneNameQuery, SpecialSomeone?>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public SpecialSomeoneNameQueryHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<SpecialSomeone?> IQueryRequest<SpecialSomeoneNameQuery, SpecialSomeone?>.Handle
    (
        SpecialSomeoneNameQuery query, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var specialSomeone = await dbContext
            .SpecialSomeone
            .Where(x => x.UniqueIdentifier == query.UniqueIdentifier)
            
            // Note: Possibly create DTO to pass data to API and select only needed fields
            // .Select(x => new DTO{x.FirstName, x.LastName, x.Nickname}) 
            
            .FirstOrDefaultAsync(cancellationToken);
        
        return specialSomeone;
    }
}
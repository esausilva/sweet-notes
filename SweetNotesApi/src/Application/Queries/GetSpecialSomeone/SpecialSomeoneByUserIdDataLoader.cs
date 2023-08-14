using Data;
using Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetSpecialSomeone;

public sealed class SpecialSomeoneByUserIdDataLoader : GroupedDataLoader<int, SpecialSomeone>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public SpecialSomeoneByUserIdDataLoader
    (
        IDbContextFactory<ApplicationDbContext> dbContextFactory, 
        IBatchScheduler batchScheduler, 
        DataLoaderOptions? options = null
    ) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<int, SpecialSomeone>> LoadGroupedBatchAsync
    (
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var specialSomeones = await dbContext.SpecialSomeone
            .Where(x => keys.Contains(x.UserId))
            .ToListAsync(cancellationToken);

        return specialSomeones.ToLookup(x => x.UserId);
    }
}
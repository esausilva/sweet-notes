using Data.Config;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.SpecialSomeone;

public class SpecialSomeoneByUserIdDataLoader : GroupedDataLoader<int, Domain.Entities.SpecialSomeone>
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

    protected override async Task<ILookup<int, Domain.Entities.SpecialSomeone>> LoadGroupedBatchAsync
    (
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        // TODO: Load notes
        var specialSomeones = await dbContext.SpecialSomeone
            .Where(x => keys.Contains(x.UserId))
            .Include(x => x.User)
            .ToListAsync(cancellationToken);

        return specialSomeones.ToLookup(x => x.UserId);
    }
}
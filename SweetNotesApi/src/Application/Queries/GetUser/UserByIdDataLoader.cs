using Data;
using Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetUser;

public sealed class UserByIdDataLoader : BatchDataLoader<int, User>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public UserByIdDataLoader
    (
        IDbContextFactory<ApplicationDbContext> dbContextFactory, 
        IBatchScheduler batchScheduler, 
        DataLoaderOptions? options = null
    ) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, User>> LoadBatchAsync
    (
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var user = await dbContext
            .FindAsync<User>(keys[0], cancellationToken);

        return new Dictionary<int, User>
        (
            new List<KeyValuePair<int, User>>
            {
                new (keys[0], user!)
            }
        );
    }
}
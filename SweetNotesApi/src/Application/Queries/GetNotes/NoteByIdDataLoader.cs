using Data.Config;
using Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetNotes;

public class NoteByIdDataLoader : BatchDataLoader<int, Note>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public NoteByIdDataLoader
    (
        IDbContextFactory<ApplicationDbContext> dbContextFactory, 
        IBatchScheduler batchScheduler, 
        DataLoaderOptions? options = null
    ) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Note>> LoadBatchAsync
    (
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var specialSomeone = await dbContext.Notes
            .Where(x => keys.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        return specialSomeone;
    }
}
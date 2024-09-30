using Application.Providers;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CreateSpecialSomeone;

public sealed class CreateSpecialSomeoneCommandHandler : ICommandRequest<CreateSpecialSomeoneCommand, SpecialSomeone>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IUniqueIdProvider<Ulid> _idProvider;

    public CreateSpecialSomeoneCommandHandler
    (
        IDbContextFactory<ApplicationDbContext> dbContextFactory, 
        IUniqueIdProvider<Ulid> snowflakeIdProvider
    )
    {
        _idProvider = snowflakeIdProvider;
        _dbContextFactory = dbContextFactory;
    }
    
    async Task<SpecialSomeone> ICommandRequest<CreateSpecialSomeoneCommand, SpecialSomeone>.Handle
    (
        CreateSpecialSomeoneCommand command, 
        CancellationToken cancellationToken
    )
    {
        var (firstName, lastName, nickName) = command;
        var specialSomeone = new SpecialSomeone
        {
            FirstName = firstName,
            LastName = lastName,
            Nickname = string.Equals(nickName?.Trim(), string.Empty) ? null : nickName,
            UniqueIdentifier = _idProvider.GenerateUniqueId().ToString(),
            UserId = command.UserId
        };
        
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await dbContext.AddAsync(specialSomeone, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return specialSomeone;
    }
}
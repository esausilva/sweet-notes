using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Application.Helpers.StringHelpers;

namespace Application.Commands.CreateSpecialSomeone;

public sealed class CreateSpecialSomeoneCommandHandler : ICommandRequest<CreateSpecialSomeoneCommand, SpecialSomeone>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public CreateSpecialSomeoneCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
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
            Nickname = nickName,
            UniqueIdentifier = GenerateRandomUrlSafeString($"{firstName}{lastName}"),
            UserId = command.UserId
        };
        
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await dbContext.AddAsync(specialSomeone, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return specialSomeone;
    }
}
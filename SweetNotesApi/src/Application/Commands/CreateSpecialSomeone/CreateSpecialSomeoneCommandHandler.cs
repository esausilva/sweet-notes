using Data.Config;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Application.Helpers.StringHelpers;

namespace Application.Commands.CreateSpecialSomeone;

public class CreateSpecialSomeoneCommandHandler : 
    ICommandRequest<(CreateSpecialSomeoneCommand command, int userId), SpecialSomeone>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public CreateSpecialSomeoneCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    async Task<SpecialSomeone> ICommandRequest<(CreateSpecialSomeoneCommand command, int userId), SpecialSomeone>
        .Handle
        (
            (CreateSpecialSomeoneCommand command, int userId) request, 
            CancellationToken cancellationToken
        )
    {
        var (firstName, lastName, nickName) = request.command;
        var specialSomeone = new SpecialSomeone
        {
            FirstName = firstName,
            LastName = lastName,
            Nickname = nickName,
            UniqueIdentifier = GenerateRandomUrlSafeString($"{firstName}{lastName}"),
            UserId = request.userId
        };
        
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        dbContext.Add(specialSomeone);
        await dbContext.SaveChangesAsync(cancellationToken);
        await dbContext.FindAsync<User>(request.userId);

        specialSomeone.Notes = new List<Note>();

        return specialSomeone;
    }
}
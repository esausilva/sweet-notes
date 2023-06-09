using Data.Config;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Application.Helpers.StringHelpers;

namespace Application.Commands.SpecialSomeone;

public class CreateSpecialSomeoneCommandHandler : 
    ICommandRequest<CreateSpecialSomeoneCommand, Domain.Entities.SpecialSomeone>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public CreateSpecialSomeoneCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    async Task<Domain.Entities.SpecialSomeone>
        ICommandRequest<CreateSpecialSomeoneCommand, Domain.Entities.SpecialSomeone>
        .Handle(CreateSpecialSomeoneCommand request, CancellationToken cancellationToken)
    {
        const int userId = 49; // TODO: Get from user authorization claims
        var specialSomeone = new Domain.Entities.SpecialSomeone
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Nickname = request.NickName,
            UniqueIdentifier = GenerateRandomUrlSafeString($"{request.FirstName}{request.LastName}"),
            UserId = userId
        };
        
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        dbContext.Add(specialSomeone);
        await dbContext.SaveChangesAsync(cancellationToken);
        dbContext.Find<User>(userId);

        specialSomeone.Notes = new List<Note>();

        return specialSomeone;
    }
}
using Data.Config;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.UserLogin;

public class UserLoginCommandHandler : ICommandRequest<UserLoginCommand, User?>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UserLoginCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<User?> ICommandRequest<UserLoginCommand, User?>.Handle
    (
        UserLoginCommand command, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var userSet = dbContext.Set<User>();
        var user = await userSet
            .Where(x => x.EmailAddress == command.EmailAddress)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is not null && BCrypt.Net.BCrypt.EnhancedVerify(command.Password, user!.Password))
        {
            return user;
        }

        return default;
    }
}
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.UserLogin;

public sealed class UserLoginQueryHandler : IQueryRequest<UserLoginQuery, User?>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UserLoginQueryHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<User?> IQueryRequest<UserLoginQuery, User?>.Handle
    (
        UserLoginQuery query, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var userSet = dbContext.Set<User>();
        var user = await userSet
            .Where(x => x.EmailAddress == query.EmailAddress)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is not null && BCrypt.Net.BCrypt.EnhancedVerify(query.Password, user!.Password))
        {
            return user;
        }

        return default;
    }
}
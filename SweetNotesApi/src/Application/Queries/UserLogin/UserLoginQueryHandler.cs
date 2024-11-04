using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.UserLogin;

public sealed class UserLoginQueryHandler : IQueryRequest<UserLoginQuery, UserLoginResponse?>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UserLoginQueryHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<UserLoginResponse?> IQueryRequest<UserLoginQuery, UserLoginResponse?>.Handle
    (
        UserLoginQuery query, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var userSet = dbContext.Set<User>();
        
        var user = await userSet
            .AsNoTracking()
            .Where(x => x.EmailAddress == query.EmailAddress)
            .Select(x => new UserLoginResponse(x.Id, x.Password, x.FirstName, x.LastName))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is not null && BCrypt.Net.BCrypt.EnhancedVerify(query.Password, user!.Password))
        {
            return user;
        }

        return default;
    }
}
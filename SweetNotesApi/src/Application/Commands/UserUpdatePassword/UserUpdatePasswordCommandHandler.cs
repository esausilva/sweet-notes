using Api.Exceptions;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.UserUpdatePassword;

public sealed class UserUpdatePasswordCommandHandler : ICommandRequest<UserUpdatePasswordCommand, bool>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UserUpdatePasswordCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<bool> ICommandRequest<UserUpdatePasswordCommand, bool>.Handle
    (
        UserUpdatePasswordCommand command, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var userSet = dbContext.Set<User>();
        var user = await userSet
            .Where(x => x.EmailAddress == command.EmailAddress)
            .FirstAsync(cancellationToken);

        if (BCrypt.Net.BCrypt.EnhancedVerify(command.NewPassword, user.Password))
            throw new CannotUpdateSamePasswordException();
        
        if (BCrypt.Net.BCrypt.EnhancedVerify(command.CurrentPassword, user.Password) is false)
            throw new PasswordMismatchException();
        
        user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(command.NewPassword);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
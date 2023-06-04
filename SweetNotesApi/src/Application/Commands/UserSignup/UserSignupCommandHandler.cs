using Application.Exceptions;
using Data.Config;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.UserSignup;

public sealed class UserSignupCommandHandler : ICommandRequest<CreateUserSignupCommand, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public UserSignupCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<int> ICommandRequest<CreateUserSignupCommand, int>.Handle
    (
        CreateUserSignupCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailAddress = request.EmailAddress,
            Password = request.Password
        };
        
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        dbContext.Add(user);
        
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
        catch (Exception e)
        {
            var data = e.InnerException!.Data;

            if (data!.Contains("ConstraintName") && data["ConstraintName"]!.Equals("IX_User_Unique_EmailAddress"))
                throw new UserAlreadyExistsException();
                
            throw;
        }
    }
}
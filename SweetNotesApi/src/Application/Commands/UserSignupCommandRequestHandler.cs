using Application.Exceptions;
using Data.Config;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class UserSignupCommandRequestHandler : ICommandRequest<User>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public UserSignupCommandRequestHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task ICommandRequest<User>.Handle(User request)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        dbContext.Add(request);
        
        try
        {
            await dbContext.SaveChangesAsync();
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
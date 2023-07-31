using Data.Config;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CreateNote;

public class CreateNoteCommandHandler : ICommandRequest<CreateNoteCommand, Note>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public CreateNoteCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<Note> ICommandRequest<CreateNoteCommand, Note>.Handle
    (
        CreateNoteCommand command, 
        CancellationToken cancellationToken
    )
    {
        var note = new Note
        {
            Message = command.Message,
            UserId = command.UserId,
            SpecialSomeoneId = command.SpecialSomeoneId
        };
        
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await dbContext.AddAsync(note, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        await dbContext.FindAsync<User>(command.UserId);
        await dbContext.FindAsync<SpecialSomeone>(command.SpecialSomeoneId);

        // await dbContext.Notes
        //     .Where(x => x.Id == note.Id)
        //     .Include(x => x.User)
        //     .Include(x => x.SpecialSomeone)
        //     .ToListAsync(cancellationToken);

        return note;
    }
}
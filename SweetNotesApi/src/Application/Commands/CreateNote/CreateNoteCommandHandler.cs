using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CreateNote;

public sealed class CreateNoteCommandHandler : ICommandRequest<CreateNoteCommand, Note>
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

        return note;
    }
}
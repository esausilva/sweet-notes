using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.DeleteNote;

public sealed class DeleteNoteCommandHandler : ICommandRequest<DeleteNoteCommand, bool>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public DeleteNoteCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    async Task<bool> ICommandRequest<DeleteNoteCommand, bool>.Handle
    (
        DeleteNoteCommand command, 
        CancellationToken cancellationToken
    )
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        // var note = await dbContext.Notes.FindAsync([command.Id], cancellationToken);
        // if (note is null)
        //     return false;
        // dbContext.Notes.Remove(note);
        
        dbContext.Notes.Remove(new Note { Id = command.Id});
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
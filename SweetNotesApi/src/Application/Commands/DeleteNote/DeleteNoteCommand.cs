using Domain.Entities;
using HotChocolate.Types.Relay;

namespace Application.Commands.DeleteNote;

public record DeleteNoteCommand
{
    [ID(nameof(Note))]
    public int Id { get; init; }
}
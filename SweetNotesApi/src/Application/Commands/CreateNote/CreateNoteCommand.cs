using Domain.Entities;
using HotChocolate.Types.Relay;

namespace Application.Commands.CreateNote;

public record CreateNoteCommand
{
    public string Message { get; init; }
    
    [ID(nameof(SpecialSomeone))]
    public int SpecialSomeoneId { get; init; } 
    
    public int UserId { get; set; }
}
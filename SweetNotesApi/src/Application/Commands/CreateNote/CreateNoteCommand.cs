namespace Application.Commands.CreateNote;

public record CreateNoteCommand(string Message, int SpecialSomeoneId)
{
    public int UserId { get; set;  }
}
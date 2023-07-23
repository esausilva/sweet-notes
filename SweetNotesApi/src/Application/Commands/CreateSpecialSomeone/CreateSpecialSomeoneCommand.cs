namespace Application.Commands.CreateSpecialSomeone;

public record CreateSpecialSomeoneCommand
(
    string FirstName,
    string LastName,
    string? NickName = default
)
{
    public int UserId { get; set; }
}
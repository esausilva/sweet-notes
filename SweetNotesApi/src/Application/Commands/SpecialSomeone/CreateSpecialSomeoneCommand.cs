namespace Application.Commands.SpecialSomeone;

public record CreateSpecialSomeoneCommand
(
    string FirstName,
    string LastName,
    string? NickName = default
);
using Application.Commands.CreateSpecialSomeone;
using FluentValidation;

namespace Api.Validation;

public class CreateSpecialSomeoneCommandValidator : AbstractValidator<CreateSpecialSomeoneCommand>
{
    public CreateSpecialSomeoneCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty();
        
        RuleFor(c => c.LastName)
            .NotEmpty();
    }
}
using Application.Commands.CreateNote;
using FluentValidation;

namespace Api.Validation;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(c => c.Message)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(c => c.SpecialSomeoneId)
            .NotEmpty();
    }
}
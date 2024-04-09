using Api.RestEndpoints.SpecialSomeoneEndpoints.Models;
using FluentValidation;

namespace Api.Validation;

public class SpecialSomeoneIdentifierValidator : AbstractValidator<SpecialSomeoneIdentifierRequest>
{
    public SpecialSomeoneIdentifierValidator()
    {
        RuleFor(s => s.Identifier)
            .NotEmpty()
            .Length(45);
    }
}
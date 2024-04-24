using Api.RestEndpoints.SpecialSomeoneEps.Models;
using FluentValidation;

namespace Api.Validation;

public class SpecialSomeoneIdentifierValidator : AbstractValidator<SpecialSomeoneIdentifierRequest>
{
    private const string AlphanumericPattern = "^[a-zA-Z0-9]+$";
    
    public SpecialSomeoneIdentifierValidator()
    {
        RuleFor(s => s.Identifier)
            .NotEmpty()
            .Length(26)
            .Matches(AlphanumericPattern)
            .WithMessage("'Identifier' must be alphanumeric.");
    }
}
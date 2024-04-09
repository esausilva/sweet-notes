using Api.RestEndpoints.UserEndpoints.Models;
using FluentValidation;

namespace Api.Validation;

public class CreateUserSignupValidator : AbstractValidator<CreateUserSignupRequest>
{
    public CreateUserSignupValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(c => c.EmailAddress)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
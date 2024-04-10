using Api.RestEndpoints.UserEps.Models;
using FluentValidation;

namespace Api.Validation;

public class UserLoginValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginValidator()
    {
        RuleFor(c => c.EmailAddress)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
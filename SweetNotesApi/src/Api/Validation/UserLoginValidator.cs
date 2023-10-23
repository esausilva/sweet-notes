using Api.RestEndpoints.Models;
using FluentValidation;

namespace Api.Validation;

public class UserLoginValidator : AbstractValidator<UserLogin>
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
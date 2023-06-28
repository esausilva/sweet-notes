using Api.RestEndpoints.Models;
using FluentValidation;

namespace Api.Validation;

public class UserLoginValidator : AbstractValidator<UserLogin>
{
    public UserLoginValidator()
    {
        RuleFor(c => c.EmailAddress)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
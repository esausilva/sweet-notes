using Api.RestEndpoints.Models;
using FluentValidation;

namespace Api.Validation;

public class CreateUserSignupValidator : AbstractValidator<CreateUserSignup>
{
    public CreateUserSignupValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty();
        
        RuleFor(c => c.LastName)
            .NotEmpty();
        
        RuleFor(c => c.EmailAddress)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
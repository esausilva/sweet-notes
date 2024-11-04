using Api.RestEndpoints.UserEps.Models;
using FluentValidation;

namespace Api.Validation;

public class UpdatePasswordRequestValidator : AbstractValidator<UserUpdatePasswordRequest>
{
    public UpdatePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty();
        
        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}
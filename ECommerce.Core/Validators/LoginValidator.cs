using ECommerce.Core.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace ECommerce.Core.Validators;

public class LoginValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginValidator()
    {
        //Email
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is invalid")
            .MaximumLength(50).WithMessage("Email must not exceed 50 characters");
        //Password
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MaximumLength(50).WithMessage("Password must not exceed 50 characters");
        
    }
}
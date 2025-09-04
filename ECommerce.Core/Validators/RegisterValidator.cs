using ECommerce.Core.DTO;
using FluentValidation;

namespace ECommerce.Core.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequestDTO>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is invalid").MaximumLength(50).WithMessage("Email must not exceed 50 characters");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MaximumLength(50).WithMessage("Password must not exceed 50 characters");
        RuleFor(x => x.PersonName).NotEmpty().WithMessage("PersonName is required").Length(1, 50).WithMessage("PersonName must be between 1-50 characters");
        RuleFor(x => x.Gender).NotNull().WithMessage("Gender can't be null").IsInEnum().WithMessage("Gender is invalid");
    }
}
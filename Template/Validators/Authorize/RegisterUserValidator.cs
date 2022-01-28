using FluentValidation;
using Template.Models.RequestModels.Authorize;

namespace Template.Validators.Authorize;

/// <summary>
/// Проверяет модель регистрации пользователя
/// </summary>
public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(36)
            .Must(x => CommonValidatorContants.ValidatePasswordRegex.Matches(x).Any());
    }
}
using Api.Models.RequestModels.Authorize;
using FluentValidation;

namespace Api.Validators.Authorize;

/// <summary>
/// Проверяет модель регистрации пользователя
/// </summary>
public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(CommonValidatorContants.PasswordMinLength)
            .MaximumLength(CommonValidatorContants.PasswordMaxLength)
            .Must(x => CommonValidatorContants.ValidatePasswordRegex.Matches(x).Any());
    }
}
using Api.Models.RequestModels.Authorize;
using FluentValidation;

namespace Api.Validators.Authorize;

/// <summary>
/// Проверяет данные пользователя при для получения токена
/// </summary>
public class LoginUserModelValidator : AbstractValidator<LoginUserModel>
{
    public LoginUserModelValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password)
            .MinimumLength(CommonValidatorContants.PasswordMinLength)
            .MaximumLength(CommonValidatorContants.PasswordMaxLength);
    }
}
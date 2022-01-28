using FluentValidation;
using Template.Models.RequestModels.Authorize;

namespace Template.Validators.Authorize;

/// <summary>
/// Проверяет данные пользователя при для получения токена
/// </summary>
public class LoginUserModelValidator : AbstractValidator<LoginUserModel>
{
    public LoginUserModelValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8).MaximumLength(36);
    }
}
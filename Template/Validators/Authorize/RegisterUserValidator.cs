using System.Text.RegularExpressions;
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
            .MaximumLength(32)
            .Must(x => new Regex("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{7,15}$")
                .Matches(x)
                .Any());
    }
}
using Api.Models.RequestModels.Authorize;
using FluentValidation;

namespace Api.Validators.Authorize;

/// <summary>
/// Проверка корректности модели смены пароля
/// </summary>
public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(CommonValidatorContants.PasswordMinLength)
            .MaximumLength(CommonValidatorContants.PasswordMaxLength)
            .Must(x => CommonValidatorContants.ValidatePasswordRegex.Matches(x).Any());

        RuleFor(x => x.NewPassword)
            .MinimumLength(CommonValidatorContants.PasswordMinLength)
            .MaximumLength(CommonValidatorContants.PasswordMaxLength)
            .Must((x, y) =>
                CommonValidatorContants.ValidatePasswordRegex.Matches(y).Any() &&
                x.Password != y);
    }
}
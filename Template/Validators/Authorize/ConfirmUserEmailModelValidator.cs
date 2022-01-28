using FluentValidation;
using Template.Models.RequestModels.Authorize;

namespace Template.Validators.Authorize;

/// <summary>
/// Проверяет данные для подтверждения почты
/// </summary>
public class ConfirmUserEmailModelValidator : AbstractValidator<ConfirmUserEmailModel>
{
    public ConfirmUserEmailModelValidator()
    {
        RuleFor(x=> x.Email).EmailAddress();
        RuleFor(x=>x.Token).NotEmpty();
    }
}
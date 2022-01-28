using FluentValidation;
using Template.Models.RequestModels.Authorize;

namespace Template.Validators.Authorize;

public class ConfirmUserEmailModelValidator : AbstractValidator<ConfirmUserEmailModel>
{
    public ConfirmUserEmailModelValidator()
    {
        RuleFor(x=> x.Email).EmailAddress();
        RuleFor(x=>x.Token).NotEmpty();
    }
}
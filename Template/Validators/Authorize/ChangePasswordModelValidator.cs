using FluentValidation;
using Template.Models.RequestModels.Authorize;

namespace Template.Validators.Authorize;

public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        RuleFor(x=> x.Email).EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(36)
            .Must(x => CommonValidatorContants.ValidatePasswordRegex.Matches(x).Any());
            
        RuleFor(x => x.NewPassword)
            .MinimumLength(8)
            .MaximumLength(36)
            .Must(x => CommonValidatorContants.ValidatePasswordRegex.Matches(x).Any());
    }
}
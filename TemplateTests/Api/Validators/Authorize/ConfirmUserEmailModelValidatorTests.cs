using FluentValidation.TestHelper;
using Api.Models.RequestModels.Authorize;
using Api.Validators.Authorize;
using Xunit;

namespace TemplateTests.Template.Models.Validators.Authorize;

public class ConfirmUserEmailModelValidatorTests
{
    private readonly ConfirmUserEmailModelValidator _validator;

    public ConfirmUserEmailModelValidatorTests()
    {
        _validator = new ConfirmUserEmailModelValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData("email")]
    [InlineData("email@")]
    public void ShouldHaveValidationErrorForEmail(string email)
    {
        var model = GetValidConfirmUserEmailModel();
        model.Email = email;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.Email));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void ShouldHaveValidationErrorForToken(string token)
    {
        var model = GetValidConfirmUserEmailModel();
        model.Token = token;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.Token));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForTakeEmail()
    {
        var model = GetValidConfirmUserEmailModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Email));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForTakeToken()
    {
        var model = GetValidConfirmUserEmailModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Token));
    }

    private ConfirmUserEmailModel GetValidConfirmUserEmailModel() =>
        new ConfirmUserEmailModel
        {
            Email = "email@email.ru",
            Token = "Token",
        };
}
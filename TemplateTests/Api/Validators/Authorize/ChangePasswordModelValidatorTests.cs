using FluentValidation.TestHelper;
using Api.Models.RequestModels.Authorize;
using Api.Validators.Authorize;
using Xunit;

namespace TemplateTests.Template.Models.Validators.Authorize;

public class ChangePasswordModelValidatorTests
{
    private readonly ChangePasswordModelValidator _validator;

    public ChangePasswordModelValidatorTests()
    {
        _validator = new ChangePasswordModelValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData("email")]
    [InlineData("email@")]
    public void ShouldHaveValidationErrorForEmail(string email)
    {
        var model = GetValidChangePasswordModelModel();
        model.Email = email;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.Email));
    }

    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("0A")]
    [InlineData("0Aa")]
    [InlineData("0Aa-")]
    public void ShouldHaveValidationErrorForPassword(string password)
    {
        var model = GetValidChangePasswordModelModel();
        model.Password = password;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.Password));
    }

    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("0A")]
    [InlineData("0Aa")]
    [InlineData("0Aa-")]
    public void ShouldHaveValidationErrorForNewPassword(string password)
    {
        var model = GetValidChangePasswordModelModel();
        model.NewPassword = password;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.NewPassword));
    }

    [Fact]
    public void ShouldHaveValidationErrorForNewPassword_DuplicatePassword()
    {
        var model = GetValidChangePasswordModelModel();
        model.NewPassword = model.Password;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.NewPassword));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForEmail()
    {
        var model = GetValidChangePasswordModelModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Email));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForPassword()
    {
        var model = GetValidChangePasswordModelModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Password));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForNewPassword()
    {
        var model = GetValidChangePasswordModelModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.NewPassword));
    }

    private ChangePasswordModel GetValidChangePasswordModelModel() =>
        new ChangePasswordModel
        {
            Email = "email@email.ru",
            Password = "Password1!",
            NewPassword = "NewPassword1!"
        };
}
using FluentValidation.TestHelper;
using Api.Models.RequestModels.Authorize;
using Api.Validators.Authorize;
using Xunit;

namespace TemplateTests.Template.Models.Validators.Authorize;

public class LoginUserModelValidatorTests
{
    private LoginUserModelValidator _validator;

    public LoginUserModelValidatorTests()
    {
        _validator = new LoginUserModelValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData("email")]
    [InlineData("email@")]
    public void ShouldHaveValidationErrorForEmail(string email)
    {
        var model = ValidGetLoginUserModel();
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
        var model = ValidGetLoginUserModel();
        model.Password = password;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.Password));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForTakeEmail()
    {
        var model = ValidGetLoginUserModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Email));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForTakePassword()
    {
        var model = ValidGetLoginUserModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Password));
    }

    private LoginUserModel ValidGetLoginUserModel() =>
        new LoginUserModel
        {
            Email = "email@mail.ru",
            Password = "Password1!"
        };
}

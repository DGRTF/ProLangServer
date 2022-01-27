using FluentValidation.TestHelper;
using Template.Models.RequestModels.Authorize;
using Template.Validators.Authorize;
using Xunit;

namespace TemplateTests.Template.Models.Validators.Authorize;

public class RegisterUserValidatorTests
{
    private readonly RegisterUserValidator _validator;

    public RegisterUserValidatorTests()
    {
        _validator = new RegisterUserValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData("email")]
    [InlineData("email@")]
    public void ShouldHaveValidationErrorForEmail(string email)
    {
        var model = ValidGetOrdersModel();
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
        var model = ValidGetOrdersModel();
        model.Password = password;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(nameof(model.Password));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForTakeEmail()
    {
        var model = ValidGetOrdersModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Email));
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForTakePassword()
    {
        var model = ValidGetOrdersModel();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(nameof(model.Password));
    }

    private RegisterUserModel ValidGetOrdersModel() =>
        new RegisterUserModel
        {
            Email = "email@mail.ru",
            Password = "Password1!"
        };
}
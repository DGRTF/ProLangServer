using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Http;
using Moq;
using Api.Models.RequestModels.Authorize;
using UserLogic.Models;
using UserLogic.Services.Interfaces;
using Xunit;
using Api.Models.Configure;
using Autofac;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Api.Controllers;

namespace TemplateTests.Template.Controllers;

public class AuthorizeControllerTests
{
    private readonly AutoMock _mock;
    private readonly AuthorizeController _authorize;

    private readonly RegisterUserModel _registerUser = new()
    {
        Email = "Email@email.ru",
        Password = "Password",
    };

    private readonly ChangePasswordModel _changePasswordModel = new()
    {
        Email = "Email@email.ru",
        Password = "Password",
        NewPassword = "newPassword",
    };

    public AuthorizeControllerTests()
    {
        var hostOptions = new HostOptions { IpAddress = "http://site.com" };
        _mock = AutoMock.GetLoose(cfg => cfg.RegisterInstance(hostOptions).As<HostOptions>());
        _authorize = _mock.Create<AuthorizeController>();
    }

    [Fact]
    public async Task RegisterUser_AuthorizeServiceRegisterUserReturnUnsuccessResult_Response400()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.RegisterUser(It.IsAny<RegisterUser>()))
            .Returns(Task.FromResult(new RegisterUserResponse(errorMessage)));

        var response = await _authorize.RegisterUser(_registerUser);
        var actual = response as BadRequestObjectResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status400BadRequest);
        Assert.Equal(actual.Value, errorMessage);
    }

    [Fact]
    public async Task RegisterUser_AuthorizeServiceRegisterUserReturnSuccessResult_SendMessageToEmail()
    {
        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.RegisterUser(It.IsAny<RegisterUser>()))
            .Returns(Task.FromResult(new RegisterUserResponse(true, "token")));

        await _authorize.RegisterUser(_registerUser);

        _mock.Mock<IConfirmMailService>().Verify(x => x.SendMessage(It.IsAny<string>(), _registerUser.Email));
    }

    [Fact]
    public async Task ForgotPassword_AuthorizeServiceForgotPasswordReturnUnsuccessResult_Response400()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ForgotPassword(It.IsAny<string>()))
            .Returns(Task.FromResult(new RegisterUserResponse(errorMessage)));

        var response = await _authorize.ForgotPassword(_registerUser.Email);
        var actual = response as BadRequestObjectResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status400BadRequest);
        Assert.Equal(actual.Value, errorMessage);
    }

    [Fact]
    public async Task ForgotPassword_AuthorizeServiceForgotPasswordReturnSuccessResult_NotResponse400()
    {
        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ForgotPassword(_registerUser.Email))
            .Returns(Task.FromResult(new RegisterUserResponse(true, "token")));

        var response = await _authorize.ForgotPassword(_registerUser.Email);
        var actual = response as NoContentResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task ResetPassword_AuthorizeServiceForgotPasswordReturnUnsuccessResult_Response400()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ResetPassword(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new ResetPasswordResponse(errorMessage)));

        var response = await _authorize.ResetPassword(new ConfirmUserEmailModel());
        var actual = response as BadRequestObjectResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status400BadRequest);
        Assert.Equal(actual.Value, errorMessage);
    }

    [Fact]
    public async Task ResetPassword_AuthorizeServiceForgotPasswordReturnSuccessResult_NotResponse400()
    {
        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ResetPassword(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new ResetPasswordResponse(true)));

        var response = await _authorize.ResetPassword(new ConfirmUserEmailModel());
        var actual = response as NoContentResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task ChangePassword_AuthorizeServiceForgotPasswordReturnUnsuccessResult_Response400()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ChangePassword(It.IsAny<ChangePassword>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(errorMessage)));

        var response = await _authorize.ChangePassword(_changePasswordModel);
        var actual = (response as IConvertToActionResult).Convert() as BadRequestObjectResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status400BadRequest);
        Assert.Equal(actual.Value, errorMessage);
    }

    [Fact]
    public async Task ChangePassword_AuthorizeServiceForgotPasswordReturnSuccessResult_ReturnToken()
    {
        var token = "token";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ChangePassword(It.IsAny<ChangePassword>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(new AuthorizeUserResponse(true, new List<string>()), token)));

        var actual = await _authorize.ChangePassword(_changePasswordModel);

        Assert.Equal(actual.Value.Token, token);
    }

    [Fact]
    public async Task ConfirmEmail_AuthorizeServiceForgotPasswordReturnUnsuccessResult_Response400()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(errorMessage)));

        var response = await _authorize.ConfirmEmail(new ConfirmUserEmailModel());
        var actual = (response as IConvertToActionResult).Convert() as BadRequestObjectResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status400BadRequest);
        Assert.Equal(actual.Value, errorMessage);
    }

    [Fact]
    public async Task ConfirmEmail_AuthorizeServiceForgotPasswordReturnSuccessResult_ReturnToken()
    {
        var token = "token";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(new AuthorizeUserResponse(true, new List<string>()), token)));

        var actual = await _authorize.ConfirmEmail(new ConfirmUserEmailModel());

        Assert.Equal(actual.Value.Token, token);
    }

    [Fact]
    public async Task GetToken_AuthorizeServiceForgotPasswordReturnUnsuccessResult_Response400()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.Login(It.IsAny<LoginUser>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(errorMessage)));

        var response = await _authorize.GetToken(new LoginUserModel());
        var actual = (response as IConvertToActionResult).Convert() as BadRequestObjectResult;

        Assert.Equal(actual.StatusCode, StatusCodes.Status400BadRequest);
        Assert.Equal(actual.Value, errorMessage);
    }

    [Fact]
    public async Task GetToken_AuthorizeServiceForgotPasswordReturnSuccessResult_ReturnToken()
    {
        var token = "token";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.Login(It.IsAny<LoginUser>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(new AuthorizeUserResponse(true, new List<string>()), token)));

        var actual = await _authorize.GetToken(new LoginUserModel());

        Assert.Equal(actual.Value.Token, token);
    }
}
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Http;
using Moq;
using Template.Models.RequestModels.Authorize;
using UserLogic.Models;
using UserLogic.Services.Interfaces;
using Xunit;
using Template.Models.Configure;
using Autofac;
using System.Collections.Generic;

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
    public async Task RegisterUser_AuthorizeServiceRegisterUserReturnUnsuccessResult_ThrowException()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.RegisterUser(It.IsAny<RegisterUser>()))
            .Returns(Task.FromResult(new RegisterUserResponse(errorMessage)));

        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => _authorize.RegisterUser(_registerUser));

        Assert.Equal(exception.Message, errorMessage);
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
    public async Task ForgotPassword_AuthorizeServiceForgotPasswordReturnUnsuccessResult_ThrowException()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ForgotPassword(It.IsAny<string>()))
            .Returns(Task.FromResult(new RegisterUserResponse(errorMessage)));

        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => _authorize.ForgotPassword(_registerUser.Email));

        Assert.Equal(exception.Message, errorMessage);
    }

    [Fact]
    public async Task ForgotPassword_AuthorizeServiceForgotPasswordReturnSuccessResult_NotThrowException()
    {
        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ForgotPassword(_registerUser.Email))
            .Returns(Task.FromResult(new RegisterUserResponse(true, "token")));

        await _authorize.ForgotPassword(_registerUser.Email);
    }
    
    [Fact]
    public async Task ResetPassword_AuthorizeServiceForgotPasswordReturnUnsuccessResult_ThrowException()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ResetPassword(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new ResetPasswordResponse(errorMessage)));

        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => _authorize.ResetPassword(new ConfirmUserEmailModel()));

        Assert.Equal(exception.Message, errorMessage);
    }

    [Fact]
    public async Task ResetPassword_AuthorizeServiceForgotPasswordReturnSuccessResult_NotThrowException()
    {
        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ResetPassword(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new ResetPasswordResponse(true)));

        await _authorize.ResetPassword(new ConfirmUserEmailModel());
    }

    [Fact]
    public async Task ChangePassword_AuthorizeServiceForgotPasswordReturnUnsuccessResult_ThrowException()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ChangePassword(It.IsAny<ChangePassword>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(errorMessage)));

        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => _authorize.ChangePassword(_changePasswordModel));

        Assert.Equal(exception.Message, errorMessage);
    }

    [Fact]
    public async Task ChangePassword_AuthorizeServiceForgotPasswordReturnSuccessResult_ReturnToken()
    {
        var token = "token";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ChangePassword(It.IsAny<ChangePassword>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(new AuthorizeUserResponse(true, new List<string>()), token)));

        var actual = await _authorize.ChangePassword(_changePasswordModel);

        Assert.Equal(actual.Token, token);
    }

    [Fact]
    public async Task ConfirmEmail_AuthorizeServiceForgotPasswordReturnUnsuccessResult_ThrowException()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(errorMessage)));

        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => _authorize.ConfirmEmail(new ConfirmUserEmailModel()));

        Assert.Equal(exception.Message, errorMessage);
    }

    [Fact]
    public async Task ConfirmEmail_AuthorizeServiceForgotPasswordReturnSuccessResult_ReturnToken()
    {
        var token = "token";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(new AuthorizeUserResponse(true, new List<string>()), token)));

        var actual = await _authorize.ConfirmEmail(new ConfirmUserEmailModel());

        Assert.Equal(actual.Token, token);
    }
    
    [Fact]
    public async Task GetToken_AuthorizeServiceForgotPasswordReturnUnsuccessResult_ThrowException()
    {
        var errorMessage = "errorMessage";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.Login(It.IsAny<LoginUser>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(errorMessage)));

        var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => _authorize.GetToken(new LoginUserModel()));

        Assert.Equal(exception.Message, errorMessage);
    }

    [Fact]
    public async Task GetToken_AuthorizeServiceForgotPasswordReturnSuccessResult_ReturnToken()
    {
        var token = "token";

        _mock.Mock<IAuthorizeService>()
            .Setup(x => x.Login(It.IsAny<LoginUser>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(new AuthorizeUserResponse(true, new List<string>()), token)));

        var actual = await _authorize.GetToken(new LoginUserModel());

        Assert.Equal(actual.Token, token);
    }
}
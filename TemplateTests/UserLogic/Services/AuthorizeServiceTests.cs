using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;
using UserLogic.ExternalInterfaces;
using UserLogic.Models;
using UserLogic.Services;
using UserLogic.Services.Interfaces;
using Xunit;

namespace TemplateTests.UserLogic.Services;

public class AuthorizeServiceTests
{
    private readonly AutoMock _mock;
    private readonly AuthorizeService _authorizeService;

    public AuthorizeServiceTests()
    {
        _mock = AutoMock.GetLoose();
        _authorizeService = _mock.Create<AuthorizeService>();
    }

    [Fact]
    public async Task ConfirmEmail_RepositoryConfirmEmailReturnFalse_ReturnEmptyToken()
    {
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(false, new[] { "User" })));

        var expected = "returnToken";

        _mock.Mock<IJwtGenerator>()
            .Setup(x => x.GetJwt(It.IsAny<IReadOnlyList<Claim>>()))
            .Returns(expected);

        var actual = await _authorizeService.ConfirmEmail(new ConfirmUserEmail());

        Assert.Equal(actual.Token, string.Empty);
    }

    [Fact]
    public async Task ConfirmEmail_RepositoryConfirmEmailReturnTrue_ReturnNotEmptyToken()
    {
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(true, new[] { "User" })));

        var expected = "returnToken";

        _mock.Mock<IJwtGenerator>()
            .Setup(x => x.GetJwt(It.IsAny<IReadOnlyList<Claim>>()))
            .Returns(expected);

        var actual = await _authorizeService.ConfirmEmail(new ConfirmUserEmail());

        Assert.Equal(actual.Token, expected);
    }


    [Fact]
    public async Task Login_RepositoryLoginReturnFalse_ReturnEmptyToken()
    {
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.Login(It.IsAny<LoginUser>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(false, new[] { "User" })));

        var expected = "returnToken";

        _mock.Mock<IJwtGenerator>()
            .Setup(x => x.GetJwt(It.IsAny<IReadOnlyList<Claim>>()))
            .Returns(expected);

        var actual = await _authorizeService.Login(new LoginUser());

        Assert.Equal(actual.Token, string.Empty);
    }

    [Fact]
    public async Task Login_RepositoryLoginReturnTrue_ReturnNotEmptyToken()
    {
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.Login(It.IsAny<LoginUser>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(true, new[] { "User" })));

        var expected = "returnToken";

        _mock.Mock<IJwtGenerator>()
            .Setup(x => x.GetJwt(It.IsAny<IReadOnlyList<Claim>>()))
            .Returns(expected);

        var actual = await _authorizeService.Login(new LoginUser());

        Assert.Equal(actual.Token, expected);
    }

    [Fact]
    public async Task ResetPassword_RepositorySuccessReset_SendPasswordToEmail()
    {
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.ResetPassword(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new ResetPasswordResponse(true)));

        var actualEmail = string.Empty;
        var actualPassword = string.Empty;

        _mock.Mock<IConfirmMailService>()
            .Setup(x => x.SendNewPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string x, string y) =>
            {
                actualPassword = x;
                actualEmail = y;

                return Task.FromResult(true);
            });

        var email = "email";

        await _authorizeService.ResetPassword(new ConfirmUserEmail { Email = email });

        Assert.Equal(email, actualEmail);
        Assert.True(!string.IsNullOrWhiteSpace(actualPassword));
    }

    [Fact]
    public async Task ResetPassword_RepositoryUnsuccessReset_NotSendPasswordToEmail()
    {
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.ResetPassword(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new ResetPasswordResponse(false)));

        var actualEmail = string.Empty;
        var actualPassword = string.Empty;

        _mock.Mock<IConfirmMailService>()
            .Setup(x => x.SendNewPassword(It.IsAny<string>(), It.IsAny<string>()));

        await _authorizeService.ResetPassword(new ConfirmUserEmail());

        Assert.ThrowsAny<Exception>(() =>
        {
            _mock.Mock<IConfirmMailService>().Verify(x => x.SendNewPassword(string.Empty, string.Empty));
        });
    }
}

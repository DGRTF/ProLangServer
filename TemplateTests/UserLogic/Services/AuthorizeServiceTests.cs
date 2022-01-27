using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;
using TemplateDataLayer.Models.Authorize;
using UserLogic.ExternalInterfaces;
using UserLogic.Models;
using UserLogic.Services;
using UserLogic.Services.Interfaces;
using Xunit;

namespace TemplateTests.UserLogic.Services;

public class AuthorizeServiceTests
{
    private readonly AutoMock _mock;
    private readonly AuthorizeService _repository;

    public AuthorizeServiceTests()
    {
        _mock = AutoMock.GetLoose();
        _repository = _mock.Create<AuthorizeService>();
    }

    [Fact]
    public async Task ServiceConfirmEmailReturnFalse_ReturnEmptyToken()
    {
        var u = new User(new Role("dg"), "email", "name");
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(false, "User")));

        var expected = "returnToken";

        _mock.Mock<IJwtGenerator>()
            .Setup(x => x.GetJwt(It.IsAny<IReadOnlyCollection<Claim>>()))
            .Returns(expected);
            
        var actual = await _repository.ConfirmEmail(new ConfirmUserEmail());

        Assert.Equal(actual, string.Empty);
    }

    [Fact]
    public async Task ServiceConfirmEmailReturnTrue_ReturnNotEmptyToken()
    {
        _mock.Mock<IAuthorizeRepository>()
            .Setup(x => x.ConfirmEmail(It.IsAny<ConfirmUserEmail>()))
            .Returns(Task.FromResult(new AuthorizeUserResponse(true, "User")));

        var expected = "returnToken";

        _mock.Mock<IJwtGenerator>()
            .Setup(x => x.GetJwt(It.IsAny<IReadOnlyCollection<Claim>>()))
            .Returns(expected);

        var actual = await _repository.ConfirmEmail(new ConfirmUserEmail());

        Assert.Equal(actual, expected);
    }
}

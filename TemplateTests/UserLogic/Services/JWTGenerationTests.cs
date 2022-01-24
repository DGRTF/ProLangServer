using System.Security.Claims;
using UserLogic.Models.JSON;
using UserLogic.Services;
using Xunit;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using FluentAssertions;
using System.Linq;

namespace TemplateTests.UserLogic.Services;

public class JWTGenerationTests
{
    private readonly JWTGeneration _generator;

    public JWTGenerationTests()
    {
        var options = new JWTAuthOptions
        {
            Key = "KeyKeyKeyKeyKeyKeyKeyKeyKeyKeyKeyKey",
            LifeTime = 15
        };

        _generator = new JWTGeneration(options);
    }

    [Fact]
    public void TestName()
    {
        var claims = new[]
        {
            new Claim("login", "login"),
            new Claim("role", "role"),
        };

        var token = _generator.GetJwt(claims);

        var payloadBase65 = token.Split(".")[1];
        var jsonPayload = Base64UrlEncoder.Decode(payloadBase65);
        var payload = JsonExtensions.DeserializeJwtPayload(jsonPayload);

        var isContainActual = claims.All(x => payload.Claims.Any(y => y.Type == x.Type && y.Value == x.Value));

        Assert.True(isContainActual);
    }
}
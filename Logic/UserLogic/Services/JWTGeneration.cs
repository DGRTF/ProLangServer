using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using UserLogic.Models.JSON;
using UserLogic.Services.Interfaces;

namespace UserLogic.Services;

/// <inheritdoc />
public class JWTGeneration : IJwtGenerator
{
    private readonly JWTAuthOptions _jwtAuthOptions;

    public JWTGeneration(JWTAuthOptions jwtAuthOptions)
    {
        _jwtAuthOptions = jwtAuthOptions;
    }

    /// <inheritdoc />
    public string GetJwt(IReadOnlyCollection<Claim> claims)
    {
        var now = DateTime.Now;

        var jwtSecurityToken = new JwtSecurityToken(
            string.Empty,
            string.Empty,
            claims,
            now,
            now.Add(TimeSpan.FromMinutes(_jwtAuthOptions.LifeTime)),
            new SigningCredentials(_jwtAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
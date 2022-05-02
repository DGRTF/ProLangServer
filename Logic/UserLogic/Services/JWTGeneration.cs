using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using UserLogic.ExtensionsMethods;
using UserLogic.Models;
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
    public TokenPairs GetJwt(IReadOnlyList<Claim> claims, Guid userId)
    {
        var now = DateTime.Now;
        var tokenExpired = now.Add(TimeSpan.FromMinutes(_jwtAuthOptions.LifeTime));

        var jwtSecurityToken = new JwtSecurityToken(
                    _jwtAuthOptions.Issuer,
                    _jwtAuthOptions.Audience,
                    claims,
                    now,
                    tokenExpired,
                    new SigningCredentials(_jwtAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var refreshClaims = new List<Claim>
        {
            new Claim(Constants.RegularTokenExpired, tokenExpired.ToUnixTimeStamp().ToString()),
            new Claim(Constants.ClaimUserIdType, userId.ToString()),
        };

        var jwtRefreshToken = new JwtSecurityToken(
                    _jwtAuthOptions.Issuer,
                    _jwtAuthOptions.Audience,
                    refreshClaims,
                    now,
                    now.Add(TimeSpan.FromMinutes(_jwtAuthOptions.RefreshLifeTime)),
                    new SigningCredentials(_jwtAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtRefreshToken);

        return new TokenPairs(token, refreshToken);
    }
}
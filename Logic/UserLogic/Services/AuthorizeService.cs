using System.Security.Claims;
using UserLogic.ExternalInterfaces;
using UserLogic.Models;
using UserLogic.Services.Interfaces;

namespace UserLogic.Services;

/// <inheritdoc />
public class AuthorizeService : IAuthorizeService
{
    private readonly IAuthorizeRepository _authorizeRepository;
    private readonly IJwtGenerator _jwtGeneration;

    public AuthorizeService(
        IAuthorizeRepository authorizeRepository,
        IJwtGenerator jwtGeneration)
    {
        _authorizeRepository = authorizeRepository;
        _jwtGeneration = jwtGeneration;
    }

    /// <inheritdoc />
    public async Task<string> RegisterUser(RegisterUser user)
    {
        var registerUserResponse = await _authorizeRepository.RegisterUser(user);

        if (!registerUserResponse.Succeeded)
            return string.Empty;

        return registerUserResponse.Token;
    }

    /// <inheritdoc />
    public async Task<string> GetToken(LoginUser user)
    {
        var loginUserResponse = await _authorizeRepository.Login(user);

        return GetJwt(user.Email, loginUserResponse);
    }

    public async Task<string> ConfirmEmail(ConfirmUserEmail model)
    {
        var loginUserResponse = await _authorizeRepository.ConfirmEmail(model);

        return GetJwt(model.Email, loginUserResponse);
    }

    private string GetJwt(string email, AuthorizeUserResponse response)
    {
        if (!response.Succeeded)
            return string.Empty;

        var claims = new[]
        {
            new Claim("email", email),
            new Claim("role", response.Role),
        };

        return _jwtGeneration.GetJwt(claims);
    }
}
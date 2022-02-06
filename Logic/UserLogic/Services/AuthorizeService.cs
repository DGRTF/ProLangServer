using System.Security.Claims;
using UserLogic.ExternalInterfaces;
using UserLogic.Models;
using UserLogic.Services.Interfaces;

namespace UserLogic.Services;

/// <inheritdoc />
public class AuthorizeService : IAuthorizeService
{
    private readonly IAuthorizeRepository _authorizeRepository;
    private readonly IConfirmMailService _mailService;
    private readonly IJwtGenerator _jwtGeneration;

    public AuthorizeService(
        IAuthorizeRepository authorizeRepository,
        IConfirmMailService mailService,
        IJwtGenerator jwtGeneration)
    {
        _authorizeRepository = authorizeRepository;
        this._mailService = mailService;
        _jwtGeneration = jwtGeneration;
    }

    /// <inheritdoc />
    public async Task<RegisterUserResponse> RegisterUser(RegisterUser user)
    {
        return await _authorizeRepository.RegisterUser(user);
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> Login(LoginUser user)
    {
        var result = await _authorizeRepository.Login(user);

        if (!result.Succeeded)
            return result;

        var token = GetJwt(user.Email, result);

        return new AuthorizeUserResponse(result, token);
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> ConfirmEmail(ConfirmUserEmail model)
    {
        var loginUserResponse = await _authorizeRepository.ConfirmEmail(model);

        if (!loginUserResponse.Succeeded)
            return loginUserResponse;

        var token = GetJwt(model.Email, loginUserResponse);

        return new AuthorizeUserResponse(loginUserResponse, token);
    }

    /// <inheritdoc />
    public async Task<AuthorizeUserResponse> ChangePassword(ChangePassword model)
    {
        var result = await _authorizeRepository.ChangePassword(model);

        if (!result.Succeeded)
            return result;

        var token = GetJwt(model.Email, result);

        return new AuthorizeUserResponse(result, token);
    }

    private string GetJwt(string email, AuthorizeUserResponse response)
    {
        if (!response.Succeeded)
            return string.Empty;

        var claims = response.Roles.Select(x => new Claim(ClaimsIdentity.DefaultRoleClaimType, x)).ToList();
        claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, email));

        return _jwtGeneration.GetJwt(claims);
    }

    /// <inheritdoc />
    public async Task<ResetPasswordResponse> ResetPassword(ConfirmUserEmail model)
    {
        model.NewPassword = Guid.NewGuid().ToString();
        var result = await _authorizeRepository.ResetPassword(model);

        if (!result.Succeeded)
            return result;

        await _mailService.SendNewPassword(model.NewPassword, model.Email);

        return result;
    }

    /// <inheritdoc />
    public async Task<RegisterUserResponse> ForgotPassword(string email)
    {
        return await _authorizeRepository.ForgotPassword(email);
    }
}
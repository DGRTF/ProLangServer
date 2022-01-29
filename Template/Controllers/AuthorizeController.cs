using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Models.RequestModels.Authorize;
using Template.Models.ResponseModels.Authorize;
using UserLogic.Models;
using UserLogic.Services.Interfaces;

/// <summary>
/// Контроллер авторизации
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/[controller]/[action]")]
public class AuthorizeController : ControllerBase
{
    private readonly IAuthorizeService _authorizeService;
    private IMapper _mapper;
    private readonly IConfirmMailService _mailSetvice;
    private readonly Template.Models.Configure.HostOptions _host;
    private readonly string _address;
    private readonly string _controller = "/api/Authorize/";

    public AuthorizeController(
        IAuthorizeService authorizeService,
        IMapper mapper,
        IConfirmMailService mailSetvice,
        Template.Models.Configure.HostOptions host)
    {
        _authorizeService = authorizeService;
        _mapper = mapper;
        _mailSetvice = mailSetvice;
        _host = host;
        _address = string.IsNullOrWhiteSpace(_host.Uri) ? $"http://{_host.IpAddress}:{_host.Port}" : _host.Uri;
        _address += _controller;
    }

    /// <summary>
    /// Получает токен авторизации
    /// </summary>
    /// <param name="user">Модель для получения токена авторизации</param>
    /// <returns>Jwt</returns>
    [HttpGet("token")]
    public async Task<SucceededAuthorize> GetToken([FromQuery] LoginUserModel user)
    {
        var mapUser = _mapper.Map<LoginUser>(user);
        var result = await _authorizeService.Login(mapUser);

        if (!result.Succeeded)
            throw new BadHttpRequestException(result.Error);

        return new SucceededAuthorize(result.Token);
    }

    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    [HttpPost]
    public async Task RegisterUser([FromBody] RegisterUserModel user)
    {
        var mapModel = _mapper.Map<RegisterUser>(user);
        var result = await _authorizeService.RegisterUser(mapModel);

        if (!result.Succeeded)
            throw new BadHttpRequestException(result.Error);

        var escapeToken = Uri.EscapeDataString(result.Token);
        var uri = new Uri(@$"{_address}ConfirmEmail?email={user.Email}&token={escapeToken}").ToString();

        await _mailSetvice.SendMessage(uri, user.Email);
    }

    /// <summary>
    /// Подтверждение email адреса
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Jwt</returns>
    [HttpGet]
    public async Task<SucceededAuthorize> ConfirmEmail([FromQuery] ConfirmUserEmailModel model)
    {
        var mapModel = _mapper.Map<ConfirmUserEmail>(model);
        var result = await _authorizeService.ConfirmEmail(mapModel);

        if (!result.Succeeded)
            throw new BadHttpRequestException(result.Error);

        return new SucceededAuthorize(result.Token);
    }

    /// <summary>
    /// Отправляет на почту ссылку для восстановления пароля
    /// </summary>
    /// <param name="email">Электронная почта пользователя</param>
    [HttpGet]
    public async Task ForgotPassword([FromQuery] string email)
    {
        var result = await _authorizeService.ForgotPassword(email);

        if (!result.Succeeded)
            throw new BadHttpRequestException(result.Error);

        var escapeToken = Uri.EscapeDataString(result.Token);
        var link = new Uri(@$"{_address}ResetPassword?email={email}&token={escapeToken}").ToString();
        await _mailSetvice.SendChangePasswordLink(link, email);
    }

    /// <summary>
    /// Отправляет на почту новый сгенерированный пароль
    /// </summary>
    /// <param name="model">Модель сброса пароля</param>
    [HttpGet]
    public async Task ResetPassword([FromQuery] ConfirmUserEmailModel model)
    {
        var mapModel = _mapper.Map<ConfirmUserEmail>(model);
        var result = await _authorizeService.ResetPassword(mapModel);

        if (!result.Succeeded)
            throw new BadHttpRequestException(result.Error);
    }

    /// <summary>
    /// Меняет пароль на новый, если правильно указан старый
    /// </summary>
    /// <param name="model">Модель смены пароля</param>
    /// <returns>Jwt</returns>
    [HttpPut]
    public async Task<SucceededAuthorize> ChangePassword([FromBody] ChangePasswordModel model)
    {
        var mapModel = _mapper.Map<ChangePassword>(model);
        var result = await _authorizeService.ChangePassword(mapModel);

        if (!result.Succeeded)
            throw new BadHttpRequestException(result.Error);

        return new SucceededAuthorize(result.Token);
    }
}
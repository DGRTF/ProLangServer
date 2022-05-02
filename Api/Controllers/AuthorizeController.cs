using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models.RequestModels.Authorize;
using Api.Models.ResponseModels.Authorize;
using UserLogic.Models;
using UserLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using UserLogic.Services;

namespace Api.Controllers;

/// <summary>
/// Контроллер авторизации
/// </summary>
[Authorize]
[Route("api/[controller]/[action]")]
public class AuthorizeController : ControllerBase
{
    private readonly IAuthorizeService _authorizeService;
    private IMapper _mapper;
    private readonly IConfirmMailService _mailSetvice;
    private readonly Api.Models.Configure.HostOptions _host;
    private readonly string _address;
    private readonly string _controller = "/api/Authorize/";

    public AuthorizeController(
        IAuthorizeService authorizeService,
        IMapper mapper,
        IConfirmMailService mailSetvice,
        Api.Models.Configure.HostOptions host)
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
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SucceededAuthorize>> GetToken([FromQuery] LoginUserModel user)
    {
        var mapUser = _mapper.Map<LoginUser>(user);
        var result = await _authorizeService.Login(mapUser);

        if (!result.Succeeded)
            return BadRequest(result.Error);

        return new SucceededAuthorize(result.TokenPairs.Token, result.TokenPairs.RefreshToken);
    }

    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserModel user)
    {
        var mapModel = _mapper.Map<RegisterUser>(user);
        var result = await _authorizeService.RegisterUser(mapModel);

        if (!result.Succeeded)
            return BadRequest(result.Error);

        var escapeToken = Uri.EscapeDataString(result.Token);
        var uri = new Uri(@$"{_address}ConfirmEmail?email={user.Email}&token={escapeToken}").ToString();
        await _mailSetvice.SendMessage(uri, user.Email);

        return Created(string.Empty, null);
    }

    /// <summary>
    /// Подтверждение email адреса
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Jwt</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SucceededAuthorize>> ConfirmEmail([FromQuery] ConfirmUserEmailModel model)
    {
        var mapModel = _mapper.Map<ConfirmUserEmail>(model);
        var result = await _authorizeService.ConfirmEmail(mapModel);

        if (!result.Succeeded)
            return BadRequest(result.Error);

        return new SucceededAuthorize(result.TokenPairs.Token, result.TokenPairs.RefreshToken);
    }

    /// <summary>
    /// Отправляет на почту ссылку для восстановления пароля
    /// </summary>
    /// <param name="email">Электронная почта пользователя</param>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ForgotPassword([FromQuery] string email)
    {
        var result = await _authorizeService.ForgotPassword(email);

        if (!result.Succeeded)
            return BadRequest(result.Error);

        var escapeToken = Uri.EscapeDataString(result.Token);
        var link = new Uri(@$"{_address}ResetPassword?email={email}&token={escapeToken}").ToString();
        await _mailSetvice.SendChangePasswordLink(link, email);

        return NoContent();
    }

    /// <summary>
    /// Отправляет на почту новый сгенерированный пароль
    /// </summary>
    /// <param name="model">Модель сброса пароля</param>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ResetPassword([FromQuery] ConfirmUserEmailModel model)
    {
        var mapModel = _mapper.Map<ConfirmUserEmail>(model);
        var result = await _authorizeService.ResetPassword(mapModel);

        if (!result.Succeeded)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Меняет пароль на новый, если правильно указан старый
    /// </summary>
    /// <param name="model">Модель смены пароля</param>
    /// <returns>Jwt</returns>
    [HttpPut]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SucceededAuthorize>> ChangePassword([FromBody] ChangePasswordModel model)
    {
        var mapModel = _mapper.Map<ChangePassword>(model);
        var result = await _authorizeService.ChangePassword(mapModel);

        if (!result.Succeeded)
            return BadRequest(result.Error);

        return new SucceededAuthorize(result.TokenPairs.Token, result.TokenPairs.RefreshToken);
    }

    /// <summary>
    /// Выдает новую пару токенов на основе токена смены
    /// Новый токен выдается в случае, если:
    /// 1. Старый токен истек
    /// 2. Токен смены для получения новой пары не истек
    /// 3. С таким токеном смены пары ни разу не обращались к этому методу
    /// </summary>
    /// <returns>Новая пара токенов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SucceededAuthorize>> RefreshTokens()
    {
        var result = await _authorizeService.RefreshTokens(User.Claims.ToList());

        return new SucceededAuthorize(result.Token, result.RefreshToken);
    }
}
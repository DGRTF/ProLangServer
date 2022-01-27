using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Models.RequestModels.Authorize;
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
    }

    /// <summary>
    /// Получает токен авторизации
    /// </summary>
    /// <param name="user">Модель для получения токена авторизации</param>
    /// <returns>Jwt</returns>
    [HttpGet("token")]
    public async Task<string> GetToken([FromQuery] LoginUserModel user)
    {
        var mapUser = _mapper.Map<LoginUser>(user);

        return await _authorizeService.GetToken(mapUser);
    }

    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    [HttpPost]
    public async Task RegisterUser([FromBody] RegisterUserModel user)
    {
        var mapModel = _mapper.Map<RegisterUser>(user);
        var token = await _authorizeService.RegisterUser(mapModel);

        if (token == string.Empty)
            throw new Exception("Внутренняя ошибка сервера");

        var address = string.IsNullOrWhiteSpace(_host.Uri) ? $"http://{_host.IpAddress}:{_host.Port}" : _host.Uri;
        var uri = new Uri(@$"{address}/api/Authorize/ConfirmEmail?email={user.Email}&token={Uri.EscapeDataString(token)}").ToString();

        var isSendMail = await _mailSetvice.SendMessage(uri, user.Email);

        if (!isSendMail)
            throw new Exception("Внутренняя ошибка сервера");
    }

    /// <summary>
    /// Подтверждение email адреса
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Jwt</returns>
    [HttpGet]
    public async Task<string> ConfirmEmail([FromQuery] ConfirmUserEmailModel model)
    {
        var mapModel = _mapper.Map<ConfirmUserEmail>(model);

        return await _authorizeService.ConfirmEmail(mapModel);
    }
}
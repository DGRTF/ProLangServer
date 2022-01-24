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

    public AuthorizeController(
        IAuthorizeService authorizeService,
        IMapper mapper)
    {
        _authorizeService = authorizeService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает токен авторизации
    /// </summary>
    /// <param name="user">Модель для получения токена авторизации</param>
    /// <returns>Jwt</returns>
    [HttpGet("token")]
    public async Task<string> GetToken([FromQuery]LoginUserModel user)
    {
        var mapUser = _mapper.Map<LoginUser>(user);

        return await _authorizeService.GetToken(mapUser);
    }

    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    /// <returns>Jwt</returns>
    [HttpPost]
    public async Task<string> RegisterUser([FromBody]RegisterUserModel user)
    {
        var mapModel = _mapper.Map<RegisterUser>(user);

        return await _authorizeService.RegisterUser(mapModel);
    }
}
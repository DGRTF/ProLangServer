using UserLogic.Models;

namespace UserLogic.Services.Interfaces;

/// <summary>
/// Сервис регистрации пользователя
/// </summary>
public interface IAuthorizeService
{
    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    /// <returns>Jwt</returns>
    Task<string> RegisterUser(RegisterUser user);

    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    /// <returns>Jwt</returns>
    Task<string> GetToken(LoginUser user);
}
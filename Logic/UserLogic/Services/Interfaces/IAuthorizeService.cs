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

    /// <summary>
    /// Проверка email пользователя
    /// </summary>
    /// <param name="user">Модель проверки email</param>
    /// <returns>Jwt</returns>
    Task<string> ConfirmEmail(ConfirmUserEmail model);
}
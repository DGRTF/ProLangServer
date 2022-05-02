using System.Security.Claims;
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
    Task<RegisterUserResponse> RegisterUser(RegisterUser user);

    /// <summary>
    /// Регистрация пользователя 
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    /// <returns>Jwt</returns>
    Task<AuthorizeUserResponse> Login(LoginUser user);

    /// <summary>
    /// Проверка email пользователя
    /// </summary>
    /// <param name="user">Модель проверки email</param>
    /// <returns>Jwt</returns>
    Task<AuthorizeUserResponse> ConfirmEmail(ConfirmUserEmail model);

    /// <summary>
    /// Меняет пароль на новый, если правильно указан старый
    /// </summary>
    /// <param name="model">Модель смены пароля</param>
    /// <returns>Jwt</returns>
    Task<AuthorizeUserResponse> ChangePassword(ChangePassword model);
    
    /// <summary>
    /// Отправляет на почту новый сгенерированный пароль
    /// </summary>
    /// <param name="model">Модель сброса пароля</param>
    Task<ResetPasswordResponse> ResetPassword(ConfirmUserEmail model);

    /// <summary>
    /// Отправляет на почту пользователя пароль
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<RegisterUserResponse> ForgotPassword(string email);

    /// <summary>
    /// Создает новую пару токенов на основе существующего токена сброса
    /// </summary>
    /// <param name="claims">Утверждения о пользователе</param>
    /// <returns>Пара токенов</returns>
    Task<TokenPairs> RefreshTokens(IReadOnlyList<Claim> claims);
}
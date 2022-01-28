using UserLogic.Models;

namespace UserLogic.ExternalInterfaces;

/// <summary>
/// Работа с базой данных при авторизации
/// </summary>
public interface IAuthorizeRepository
{
    /// <summary>
    /// Создает пользователя в базе данных
    /// </summary>
    /// <param name="user">Модель регистрации пользователя</param>
    /// <returns>Успешность регистрации пользователя</returns>
    Task<RegisterUserResponse> RegisterUser(RegisterUser user);

    /// <summary>
    /// Проверяет наличие пользователя и его пароль связываясь с базой данных
    /// </summary>
    /// <param name="user">Модель входа пользователя</param>
    /// <returns>Успешность входа пользователя</returns>
    Task<AuthorizeUserResponse> Login(LoginUser user);

    /// <summary>
    /// Проверка email пользователя
    /// </summary>
    /// <param name="user">Модель проверки email</param>
    Task<AuthorizeUserResponse> ConfirmEmail(ConfirmUserEmail model);

    /// <summary>
    /// Меняет пароль на новый, если правильно указан старый
    /// </summary>
    /// <param name="model">Модель смены пароля</param>
    /// <returns>Модель результата операции</returns>
    Task<AuthorizeUserResponse> ChangePassword(ChangePassword model);
    
    /// <summary>
    /// Задает новый пароль пользователю на основе отправленного на почту токена и email
    /// </summary>
    /// <param name="model">Модель сброса пароля</param>
    /// <returns>Модель результата операции</returns>
    Task<ResetPasswordResponse> ResetPassword(ConfirmUserEmail model);
    
    /// <summary>
    /// Возвращает сгенерированный токен для сброса пароля по электронной почте пользователя
    /// </summary>
    /// <param name="email">Электронная почта пользователя</param>
    Task<RegisterUserResponse> ForgotPassword(string email);
}
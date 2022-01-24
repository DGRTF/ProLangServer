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
    Task<AuthorizeUserResponse> RegisterUser(RegisterUser user);

    /// <summary>
    /// Проверяет наличие пользователя и его пароль связываясь с базой данных
    /// </summary>
    /// <param name="user">Модель входа пользователя</param>
    /// <returns>Успешность входа пользователя</returns>
    Task<AuthorizeUserResponse> Login(LoginUser user);
}
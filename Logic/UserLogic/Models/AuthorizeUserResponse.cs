namespace UserLogic.Models;

/// <summary>
/// Представляет ответ после попытки входа пользователя
/// </summary>
public class AuthorizeUserResponse
{
    /// <summary>
    /// Успешность операции
    /// </summary>
    public bool Succeeded { get; }

    /// <summary>
    /// В случае успеха заданные пользователю роли
    /// </summary>
    /// <value></value>
    public IReadOnlyList<string> Roles { get; }

    /// <summary>
    /// Ошибка в случае неудачи
    /// </summary>
    public string Error { get; }

    /// <summary>
    /// В случае удачи выданный пользователю токен
    /// </summary>
    /// <value></value>
    public string Token { get; }

    /// <summary>
    /// Конструктор для формирования ответа о неуспешном запросе
    /// </summary>
    /// <param name="error">Ошибка</param>
    public AuthorizeUserResponse(string error) : this(false, new List<string>())
    {
        Error = error ?? string.Empty;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="succeeded">Успешность операции регистрации</param>
    /// <param name="role">В случае успеха присвоенная пользователю роль</param>
    public AuthorizeUserResponse(bool succeeded, IReadOnlyList<string> roles)
    {
        Succeeded = succeeded;
        Roles = succeeded ? roles : new List<string>();
        Roles ??= new List<string>();
        Error = string.Empty;
        Token = string.Empty;
    }

    /// <summary>
    /// Формирует на основании успешного ответа авторизации ответ авторизации с токеномы
    /// </summary>
    /// <param name="response"></param>
    /// <param name="token"></param>
    public AuthorizeUserResponse(AuthorizeUserResponse response, string token)
    {
        Succeeded = response.Succeeded ? response.Succeeded : throw new ArgumentException($"Входной параметр {nameof(response)} должен быть true");
        Roles = response.Roles;
        Error = string.Empty;
        Token = token;
    }
}
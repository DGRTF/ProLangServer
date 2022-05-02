namespace UserLogic.Models;

/// <summary>
/// Представляет ответ после попытки входа пользователя
/// </summary>
public class AuthorizeUserResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; }

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
    public TokenPairs TokenPairs { get; }

    /// <summary>
    /// Конструктор для формирования ответа о неуспешном запросе
    /// </summary>
    /// <param name="error">Ошибка</param>
    public AuthorizeUserResponse(string error) : this(false, new List<string>(), Guid.Empty)
    {
        Error = error ?? string.Empty;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="succeeded">Успешность операции регистрации</param>
    /// <param name="role">В случае успеха присвоенная пользователю роль</param>
    public AuthorizeUserResponse(bool succeeded, IReadOnlyList<string> roles, Guid userId)
    {
        UserId = userId;
        Succeeded = succeeded;
        Roles = succeeded ? roles : new List<string>();
        Roles ??= new List<string>();
        Error = string.Empty;
        TokenPairs = new TokenPairs(string.Empty, string.Empty);
    }

    /// <summary>
    /// Формирует на основании успешного ответа авторизации ответ авторизации с токеном
    /// </summary>
    /// <param name="response">Успешный ответ аторизации</param>
    /// <param name="token">Токены авторизации</param>
    public AuthorizeUserResponse(AuthorizeUserResponse response, TokenPairs token)
    {
        Succeeded = response.Succeeded ? response.Succeeded : throw new ArgumentException($"Входной параметр {nameof(response)} должен быть true");
        UserId = response.UserId;
        Roles = response.Roles;
        Error = string.Empty;
        TokenPairs = token ?? new TokenPairs(string.Empty, string.Empty);
    }
}
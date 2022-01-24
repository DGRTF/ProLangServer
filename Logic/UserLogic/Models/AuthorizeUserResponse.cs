namespace UserLogic.Models;

/// <summary>
/// Представляет ответ после регистрации пользователя
/// </summary>
public class AuthorizeUserResponse
{
    /// <summary>
    /// Успешность операции
    /// </summary>
    public bool Succeeded { get; }

    /// <summary>
    /// В случае успеха заданная пользователю роль
    /// </summary>
    /// <value></value>
    public string Role { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="succeeded">Успешность операции регистрации</param>
    /// <param name="role">В случае успеха присвоенная пользователю роль</param>
    public AuthorizeUserResponse(bool succeeded, string role)
    {
        Succeeded = succeeded;
        Role = succeeded ? role : string.Empty;
    }
}
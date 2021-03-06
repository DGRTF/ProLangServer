namespace UserLogic.Models;

/// <summary>
/// Представляет ответ после регистрации пользователя
/// </summary>
public class RegisterUserResponse
{
    /// <summary>
    /// Успешность операции
    /// </summary>
    public bool Succeeded { get; }

    /// <summary>
    /// Возвращает токен для подтверждения регистрации
    /// </summary>
    public string Token { get; }

    /// <summary>
    /// Ошибка в случае неудачи
    /// </summary>
    public string Error { get; }

    /// <summary>
    /// Конструктор для формирования ответа о неуспешном запросе
    /// </summary>
    /// <param name="error">Ошибка</param>
    public RegisterUserResponse(string error) : this(false, string.Empty)
    {
        Error = error ?? string.Empty;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="succeeded">Успешность операции регистрации</param>
    /// <param name="role">В случае успеха токен для подтверждения регистрации</param>
    public RegisterUserResponse(bool succeeded, string token)
    {
        Succeeded = succeeded;
        Token = succeeded ? token : string.Empty;
        Error = string.Empty;
    }
}
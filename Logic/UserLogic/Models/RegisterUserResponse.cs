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
    public string Token { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="succeeded">Успешность операции регистрации</param>
    /// <param name="role">В случае успеха токен для подтверждения регистрации</param>
    public RegisterUserResponse(bool succeeded, string token)
    {
        Succeeded = succeeded;
        Token = succeeded ? token : string.Empty;
    }
}
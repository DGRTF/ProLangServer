namespace UserLogic.Models;

/// <summary>
/// Модель результата попытки сброса пароля
/// </summary>
public class ResetPasswordResponse
{
    /// <summary>
    /// Успешность операции
    /// </summary>
    public bool Succeeded { get; }

    /// <summary>
    /// Ошибка в случае неудачи
    /// </summary>
    public string Error { get; }

    /// <summary>
    /// Конструктор для формирования ответа о неуспешном запросе
    /// </summary>
    /// <param name="error">Ошибка</param>
    public ResetPasswordResponse(string error) : this(false)
    {
        Error = error ?? string.Empty;;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="succeeded">Успешность операции регистрации</param>
    public ResetPasswordResponse(bool succeeded)
    {
        Succeeded = succeeded;
        Error = string.Empty;
    }
}
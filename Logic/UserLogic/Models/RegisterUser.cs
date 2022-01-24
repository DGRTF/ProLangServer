namespace UserLogic.Models;

/// <summary>
/// Модель регистрации пользователя
/// </summary>
public class RegisterUser
{
    /// <summary>
    /// User email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User password
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
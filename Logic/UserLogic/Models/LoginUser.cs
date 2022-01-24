namespace UserLogic.Models;

/// <summary>
/// Представляет модель для получения токена
/// </summary>
public class LoginUser
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
namespace UserLogic.Models;

/// <summary>
/// Модель смены пароля
/// </summary>
public class ChangePassword
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User password
    /// </summary>
    /// <value></value>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// New user password
    /// </summary>
    /// <value></value>
    public string NewPassword { get; set; } = string.Empty;
}